import { IpcRenderer } from "electron";
import { action, makeObservable, observable } from "mobx";
import { DownloadProcessDetail } from "../../commons/dto/download-process-detail";
import { videoInfo } from "ytdl-core";
import Logger from "electron-log/renderer";
import { utils } from "../../commons/utils";

interface SongStoreProps {
    ipcRenderer: IpcRenderer;
    on: (
        channel: string,
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        listener: (event: Electron.IpcRendererEvent, ...args: any[]) => void
    ) => Electron.IpcRenderer;
}
export class SongStore {
    ipcRenderer: IpcRenderer;
    on: (
        channel: string,
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        listener: (event: Electron.IpcRendererEvent, ...args: any[]) => void
    ) => Electron.IpcRenderer;

    @observable
    videoInfo: videoInfo | null = null;

    @observable
    downloadPercentage = 0;

    @observable
    estimatedDownloadTime = 0;

    @observable
    isDownloading = false;

    constructor(props: SongStoreProps) {
        makeObservable(this);

        this.ipcRenderer = props.ipcRenderer;
        this.on = props.on;
    }

    @action
    async getVideoInfo(url: string): Promise<videoInfo> {
        const info: videoInfo = await this.ipcRenderer.invoke("get-song-info", { url: url });
        this.setVideoInfo(info);
        Logger.info(
            `Received info from ${url}. Title: %s. Length: %s`,
            info.videoDetails.title,
            utils.formatTime(parseInt(info.videoDetails.lengthSeconds))
        );
        Logger.info(info.videoDetails.author);
        return info;
    }

    @action
    async downloadSongFromInfo(info: videoInfo) {
        this.isDownloading = true;
        Logger.info(`Send download request for "${info.videoDetails.title}"`);
        this.ipcRenderer.invoke("download-song", { info: info });

        this.on("download-song/process", (_, arg: DownloadProcessDetail) => {
            this.setDownloadPercentage(arg.percentage);
            this.setEstimatedDownloadTime(arg.estimatedDownloadTime);
        });

        this.on("download-song/complete", (_, arg: DownloadProcessDetail) => {
            this.setDownloadPercentage(arg.percentage);
            this.setEstimatedDownloadTime(arg.estimatedDownloadTime);
            this.isDownloading = false;
        });
    }

    @action
    cancelDownload() {
        this.ipcRenderer.send("download-song/cancel");

        this.isDownloading = false;
    }

    @action
    setVideoInfo(videoInfo: videoInfo) {
        this.videoInfo = videoInfo;
    }

    @action
    setDownloadPercentage(percentage: number) {
        this.downloadPercentage = percentage;
    }

    @action
    setEstimatedDownloadTime(time: number) {
        this.estimatedDownloadTime = time;
    }

    @action
    setIsDownloading(isDownloading: boolean) {
        this.isDownloading = isDownloading;
    }
}
