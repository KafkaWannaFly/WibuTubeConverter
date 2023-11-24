import Logger from "electron-log";
import fs from "fs";
import ytdl from "ytdl-core";
import { DownloadProcessDetail } from "../../commons/dto/download-process-detail";
import { DownloadRequest } from "../../commons/dto/download-request";

const DOWNLOAD_DIR = "./downloads";

export const musicEndPoint = (ipcMain: Electron.IpcMain) => {
    fs.mkdirSync(DOWNLOAD_DIR, { recursive: true });

    ipcMain.handle("get-song-info", async (_, arg: DownloadRequest) => {
        const url = arg.url;
        Logger.info(`Getting info from ${url}`);
        return await ytdl.getInfo(url);
    });

    ipcMain.handle("download-song", async (e, arg: DownloadRequest) => {
        const info = arg.info;
        Logger.info(`Downloading ${info.videoDetails.title}`);

        let startTime = 0;

        const downloadStream = ytdl.downloadFromInfo(info, { filter: "audioandvideo" });

        const eventEmitter = e.sender;

        downloadStream.pipe(fs.createWriteStream(`${DOWNLOAD_DIR}/${info.videoDetails.title}.mp4`));

        downloadStream.once("response", () => {
            startTime = Date.now();
        });

        downloadStream.on("progress", (_, downloaded: number, total: number) => {
            const percentage = downloaded / total;
            const downloadedMinutes = (Date.now() - startTime) / 1000 / 60;
            const estimatedDownloadTime = downloadedMinutes / percentage - downloadedMinutes;

            eventEmitter.send("download-song/process", {
                percentage: percentage,
                estimatedDownloadTime: estimatedDownloadTime,
                title: info.videoDetails.title,
            } as DownloadProcessDetail);
        });

        downloadStream.on("end", () => {
            Logger.info(`Download complete: ${info.videoDetails.title}`);
            eventEmitter.send("download-song/complete", {
                title: info.videoDetails.title,
                percentage: 1,
                estimatedDownloadTime: 0,
            } as DownloadProcessDetail);
        });

        downloadStream.on("error", (err) => {
            Logger.error(err);
        });

        ipcMain.on("download-song/cancel", () => {
            Logger.info("Download canceled!");
            downloadStream.destroy();
        });
    });
};
