import { Image, Typography } from "antd";
import Title from "antd/es/typography/Title";
import React from "react";

interface SongBasicInfoProps {
    image: string;
    name: string;
    performer: string;
}

export const SongBasicInfo = (props: SongBasicInfoProps) => {
    const { image, name, performer } = props;
    return (
        <div>
            <Image src={image} fallback="../../assests/logo.png" preview={false} placeholder width="100%" />
            <Title level={4}>{name}</Title>
            <Typography.Text>{performer}</Typography.Text>
        </div>
    );
};
