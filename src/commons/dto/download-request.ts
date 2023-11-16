import { videoInfo } from "ytdl-core";

export interface DownloadRequest {
    url: string;
    info: videoInfo;
}
