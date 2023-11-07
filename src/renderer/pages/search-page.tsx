import { Form, Image } from "antd";
import { useForm } from "antd/es/form/Form";
import Search from "antd/es/input/Search";
import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { utils } from "../../commons/utils";
import { StoreContext } from "../app";

export const SearchPage = () => {
    const [url, setUrl] = useState("");

    const [form] = useForm();

    const navigate = useNavigate();

    const { navigationStore } = useContext(StoreContext);

    return (
        <>
            <Image src="./src/renderer/assests/main-page.jpg" preview={false} />
            <Form form={form}>
                <Form.Item
                    name="url"
                    rules={[
                        {
                            required: true,
                            validator(_, value) {
                                return utils.isValidUrl(value)
                                    ? Promise.resolve()
                                    : Promise.reject("Please input valid URL!");
                            },
                        },
                    ]}
                >
                    <Search
                        placeholder="Enter YouTube URL"
                        enterButton
                        value={url}
                        onChange={(e) => {
                            setUrl(e.target.value);
                        }}
                        onSearch={async () => {
                            try {
                                const fields = await form.validateFields();
                                const url = fields.url as string;

                                navigationStore.addHistory(url);
                                navigate(`/detail`, { state: { url } });
                            } catch (error) {
                                console.error(error);
                            }
                        }}
                    />
                </Form.Item>
            </Form>
        </>
    );
};
