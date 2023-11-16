import { IpcRenderer } from "electron";
import { action, makeObservable, observable } from "mobx";
import { DownloadProcessDetail } from "../../commons/dto/download-process-detail";
import { videoInfo } from "ytdl-core";

export class SongStore {
    ipcRenderer: IpcRenderer;

    @observable
    videoInfo: videoInfo | null = null;

    @observable
    downloadPercentage = 0;

    @observable
    estimatedDownloadTime = 0;

    @observable
    isDownloading = false;

    constructor(ipcRenderer: IpcRenderer) {
        makeObservable(this);

        this.ipcRenderer = ipcRenderer;
    }

    @action
    async getVideoInfo(url: string): Promise<videoInfo> {
        const info: videoInfo = await this.ipcRenderer.invoke("get-song-info", { url: url });
        this.setVideoInfo(info);
        return info;
    }

    @action
    async downloadSongFromInfo(info: videoInfo) {
        this.isDownloading = true;
        this.ipcRenderer.invoke("download-song", { info: info });

        this.ipcRenderer
            .on("download-song/process", (_, arg: DownloadProcessDetail) => {
                this.setDownloadPercentage(arg.percentage);
                this.setEstimatedDownloadTime(arg.estimatedDownloadTime);
            })
            .on("download-song/complete", (_, arg: DownloadProcessDetail) => {
                this.setDownloadPercentage(arg.percentage);
                this.setEstimatedDownloadTime(arg.estimatedDownloadTime);
                this.isDownloading = false;
            });
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
