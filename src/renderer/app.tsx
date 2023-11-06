import React from 'react';
import {createRoot} from 'react-dom/client';
import {createBrowserRouter, Outlet, RouterProvider} from "react-router-dom";
import {Layout} from "antd";
import {Content} from "antd/es/layout/layout";
import {SearchPage} from "./pages/search-page";

const root = createRoot(document.body);
root.render(
    <React.StrictMode>
        <RouterProvider router={createBrowserRouter([
            {
                path: "/",
                element: <Layout>
                    <Content>
                        <Outlet/>
                    </Content>
                </Layout>,
                children: [
                    {
                        path: "/",
                        element: <SearchPage/>
                    }
                ]
            }
        ])}/>
    </React.StrictMode>
);