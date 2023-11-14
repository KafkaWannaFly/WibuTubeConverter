import { IpcRenderer } from "electron";
import { action, makeObservable, observable } from "mobx";
import { videoInfo } from "ytdl-core";

export class SongStore {
    ipcRenderer: IpcRenderer;

    @observable
    videoInfo: videoInfo | null = null;

    constructor(ipcRenderer: IpcRenderer) {
        makeObservable(this);

        this.ipcRenderer = ipcRenderer;
    }

    @action
    setVideoInfo(videoInfo: videoInfo) {
        this.videoInfo = videoInfo;
    }

    @action
    async getVideoInfo(url: string): Promise<videoInfo> {
        const info = await this.ipcRenderer.invoke("get-song-info", { url: url });
        this.setVideoInfo(info);
        return info;
    }
}
