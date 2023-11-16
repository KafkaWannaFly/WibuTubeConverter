import ytdl from "ytdl-core";
import fs from "fs";
import { DownloadRequest } from "../../commons/dto/download-request";
import { DownloadProcessDetail } from "../../commons/dto/download-process-detail";

export const musicEndPoint = (ipcMain: Electron.IpcMain) => {
    ipcMain.handle("get-song-info", async (_, arg: DownloadRequest) => {
        const url = arg.url;
        return await ytdl.getInfo(url);
    });

    ipcMain.handle("download-song", async (_, arg: DownloadRequest) => {
        const info = arg.info;

        let startTime = 0;
        const savedDir = "./video";
        fs.mkdirSync(savedDir, { recursive: true });
        ytdl.downloadFromInfo(info, { quality: "highestaudio" })
            .pipe(fs.createWriteStream(`${savedDir}/${info.videoDetails.title}.mp4`))
            .on("response", () => {
                startTime = Date.now();
            })
            .on("progress", (_, downloaded: number, total: number) => {
                const percentage = downloaded / total;
                const downloadedMinutes = (Date.now() - startTime) / 1000 / 60;
                const estimatedDownloadTime = downloadedMinutes / percentage - downloadedMinutes;
                console.log(`${(percentage * 100).toFixed(2)}% downloaded`);
                console.log(`Estimated download time: ${estimatedDownloadTime.toFixed(2)} minutes`);

                ipcMain.emit("download-song/process", {
                    percentage: percentage,
                    estimatedDownloadTime: estimatedDownloadTime,
                    title: info.videoDetails.title,
                } as DownloadProcessDetail);
            })
            .on("end", () => {
                console.log("Download complete!");
                ipcMain.emit("download-song/complete", {
                    title: info.videoDetails.title,
                    percentage: 1,
                    estimatedDownloadTime: 0,
                } as DownloadProcessDetail);
            });
    });
};
