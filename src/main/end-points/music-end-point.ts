import ytdl from "ytdl-core";
import { DownloadRequest } from "../../commons/requests/download-request";

export const musicEndPoint = (ipcMain: Electron.IpcMain) => {
    ipcMain.handle("get-song-info", async (event, arg: DownloadRequest) => {
        const url = arg.url;
        return await ytdl.getInfo(url);
    });
};
