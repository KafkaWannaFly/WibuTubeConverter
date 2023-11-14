import { useRequest } from "ahooks";
import { Button, Modal, Skeleton, Tooltip } from "antd";
import React, { useContext, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { StoreContext } from "../../app";
import { SongBasicInfo } from "./song-basic-info";

export const SongDetailPage = () => {
    const [isOpen, setIsOpen] = useState(true);

    const navigate = useNavigate();
    const { state } = useLocation();
    const url = state.url as string;

    const { songStore, navigationStore } = useContext(StoreContext);

    const { loading, data, error } = useRequest(() => songStore.getVideoInfo(url));

    const modelFooter = [
        <Tooltip title="It would stop without second confirm 😉">
            <Button danger onClick={() => onCancel()}>
                Cancel
            </Button>
        </Tooltip>,
    ];

    const onCancel = () => {
        setIsOpen(false);
        navigate(`/`);
        navigationStore.popHistory();
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
                        image={data.videoDetails.thumbnails[0].url}
                        name={data.videoDetails.title}
                        performer={data.videoDetails.author.name}
                    />
                )}
                {error && JSON.stringify(error, null, 2)}
            </Modal>
        </div>
    );
};
