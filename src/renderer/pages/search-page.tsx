import { Form, Image } from "antd";
import { useForm } from "antd/es/form/Form";
import Search from "antd/es/input/Search";
import useMessage from "antd/es/message/useMessage";
import React, { useState } from "react";
import { utils } from "../../commons/utils";

export const SearchPage = () => {
    const [url, setUrl] = useState("");

    const [form] = useForm();
    const [messageApi, contextHolder] = useMessage();
    return (
        <>
            <Image src="./src/renderer/assests/main-page.jpg" preview={false} />
            <Form form={form}>
                <Form.Item
                    name="url"
                    noStyle
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
                                console.log(url);
                            } catch (error) {
                                messageApi.error(error.errorFields[0].errors[0], 1);
                            }
                        }}
                    />
                </Form.Item>
            </Form>
            {contextHolder}
        </>
    );
};
