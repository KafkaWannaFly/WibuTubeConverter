import { blue } from "@ant-design/colors";
import { LeftOutlined } from "@ant-design/icons";
import { Button } from "antd";
import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";

export const Navbar = () => {
    const [displayMode, setDisplayMode] = useState("block");
    const location = useLocation();

    useEffect(() => {
        if (location.pathname === "/") {
            setDisplayMode("none");
        } else {
            setDisplayMode("block");
        }
    }, [location]);

    return (
        <div>
            <Button
                type="text"
                size="large"
                icon={<LeftOutlined style={{ color: blue.primary }} />}
                style={{ display: displayMode }}
            />
        </div>
    );
};
