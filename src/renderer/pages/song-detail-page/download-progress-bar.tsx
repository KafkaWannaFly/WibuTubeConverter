import { useRequest } from "ahooks";
import React, { useContext } from "react";
import { StoreContext } from "../../app";
import { videoInfo } from "ytdl-core";
import { Progress, Typography } from "antd";
import { Observer } from "mobx-react";

interface DownloadProgressBarProps {
    info: videoInfo;
}

export const DownloadProgressBar = (props: DownloadProgressBarProps) => {
    const { songStore } = useContext(StoreContext);
    const { downloadPercentage } = songStore;
    const { error, loading } = useRequest(() => songStore.downloadSongFromInfo(props.info));

    return (
        <div>
            <Observer>
                {() => (
                    <>
                        <Progress
                            percent={downloadPercentage * 100}
                            status={error ? "exception" : loading ? "normal" : "success"}
                        />
                    </>
                )}
            </Observer>
            {error && <Typography.Text type="danger">{error.message}</Typography.Text>}
        </div>
    );
};
