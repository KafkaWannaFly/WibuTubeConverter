import { useRequest } from "ahooks";
import { Button, Modal, Result, Skeleton, Tooltip } from "antd";
import React, { useContext, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { StoreContext } from "../../app";
import { SongBasicInfo } from "./song-basic-info";
import { DownloadProgressBar } from "./download-progress-bar";

export const SongDetailPage = () => {
    const [isOpen, setIsOpen] = useState(true);

    const navigate = useNavigate();
    const { state } = useLocation();
    const url = state.url as string;

    const { songStore } = useContext(StoreContext);

    const { loading, data, error } = useRequest(() => songStore.getVideoInfo(url));

    const modelFooter = [
        <Tooltip key={0} title="It would stop without any second confirmation 😉">
            <Button danger onClick={() => onCancel()}>
                Back
            </Button>
        </Tooltip>,
    ];

    const onCancel = () => {
        songStore.cancelDownload();
        setIsOpen(false);
        navigate(`/`);
    };

    return (
        <div>
            <Modal
                title="Loading Info and Estimation..."
                centered
                open={isOpen}
                closable={false}
                onCancel={() => setIsOpen(false)}
                footer={modelFooter}
                maskClosable={false}
            >
                <Skeleton active loading={loading} />
                {!loading && data && (
                    <SongBasicInfo
                        image={data.videoDetails.thumbnails.pop()?.url}
                        name={data.videoDetails.title}
                        performer={data.videoDetails.author.name}
                    />
                )}
                {!loading && data && <DownloadProgressBar info={data} />}
                {!loading && error && <Result status="error" subTitle={error.message} />}
            </Modal>
        </div>
    );
};
