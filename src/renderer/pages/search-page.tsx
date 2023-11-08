import { Form, Image } from "antd";
import { useForm } from "antd/es/form/Form";
import Search from "antd/es/input/Search";
import { Observer } from "mobx-react";
import React, { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { utils } from "../../commons/utils";
import { StoreContext } from "../app";

export const SearchPage = () => {
    const [form] = useForm();

    const navigate = useNavigate();

    const { navigationStore, searchStore } = useContext(StoreContext);

    const handleSearch = async () => {
        const ipcRenderer = window.api.ipcRenderer;
        const r = await ipcRenderer.invoke("get-song", { url: searchStore.url });
        console.log(r);

        try {
            const fields = await form.validateFields();
            const url = fields.url as string;

            navigationStore.addHistory(url);
            navigate(`/detail`, { state: { url } });
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div style={{ maxWidth: "36rem" }}>
            <Image src="./src/renderer/assests/main-page.jpg" preview={false} />
            <Form
                form={form}
                initialValues={{
                    url: searchStore.url,
                }}
            >
                <Observer>
                    {() => (
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
                                // ref={searchRef}
                                size="large"
                                placeholder="Enter YouTube URL"
                                enterButton
                                value={searchStore.url}
                                onChange={(e) => {
                                    searchStore.setUrl(e.target.value);
                                }}
                                onSearch={handleSearch}
                            />
                        </Form.Item>
                    )}
                </Observer>
            </Form>
        </div>
    );
};
