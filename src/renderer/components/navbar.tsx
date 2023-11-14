import { blue } from "@ant-design/colors";
import { LeftOutlined } from "@ant-design/icons";
import { Button } from "antd";
import { Observer } from "mobx-react";
import React, { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";

export const Navbar = () => {
    const naviagte = useNavigate();
    const [currentPath, setCurrentPath] = useState("/");

    const location = useLocation();
    useEffect(() => {
        setCurrentPath(location.pathname);
    }, [location]);

    return (
        <div>
            <Observer>
                {() => (
                    <Button
                        type="text"
                        size="large"
                        icon={<LeftOutlined style={{ color: blue.primary }} />}
                        style={{ display: currentPath === "/" ? "none" : "block" }}
                        onClick={() => {
                            naviagte(-1);
                        }}
                    />
                )}
            </Observer>
        </div>
    );
};
