import React, { createContext } from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, Outlet, RouterProvider } from "react-router-dom";
import { Layout } from "antd";
import { Content, Header } from "antd/es/layout/layout";
import { SearchPage } from "./pages/search-page";
import { SongDetail } from "./pages/song-detail";
import { Navbar } from "./components/navbar";
import { SearchStore } from "./stores/search-store";

const contextValue = { searchStore: new SearchStore() };
export const StoreContext = createContext(contextValue);

createRoot(document.getElementById("root")).render(
    <React.StrictMode>
        <RouterProvider
            router={createBrowserRouter([
                {
                    path: "/",
                    element: (
                        <StoreContext.Provider value={contextValue}>
                            <Layout style={{ background: "transparent" }}>
                                <Header style={{}}>
                                    <Navbar />
                                </Header>
                                <Content style={{ padding: "2rem" }}>
                                    <Outlet />
                                </Content>
                            </Layout>
                        </StoreContext.Provider>
                    ),
                    children: [
                        {
                            path: "/",
                            element: <SearchPage />,
                        },
                        {
                            path: "detail",
                            element: <SongDetail />,
                        },
                    ],
                },
            ])}
        />
    </React.StrictMode>
);
