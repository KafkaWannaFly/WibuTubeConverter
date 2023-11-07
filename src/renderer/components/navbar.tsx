import { blue } from "@ant-design/colors";
import { LeftOutlined } from "@ant-design/icons";
import { Button } from "antd";
import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { StoreContext } from "../app";
import { Observer } from "mobx-react";

export const Navbar = () => {
    const naviagte = useNavigate();
    const { navigationStore } = useContext(StoreContext);

    return (
        <div>
            <Observer>
                {() => (
                    <Button
                        type="text"
                        size="large"
                        icon={<LeftOutlined style={{ color: blue.primary }} />}
                        style={{ display: navigationStore.history.length === 0 ? "none" : "block" }}
                        onClick={() => {
                            navigationStore.popHistory();
                            naviagte(-1);
                        }}
                    />
                )}
            </Observer>
        </div>
    );
};
