import { configureStore } from "@reduxjs/toolkit";
import { createBlogApi } from "./features/create-blog/create-blog-api";
import { singleBlogApi } from "./features/blog-detail/blog-detail";
import { blogsApi } from "./features/blogs/blogs-api";
import { authApi } from "./features/auth";

export const store = configureStore({
  reducer: {
    [blogsApi.reducerPath]: blogsApi.reducer,
    [singleBlogApi.reducerPath]: singleBlogApi.reducer,
    [createBlogApi.reducerPath]: createBlogApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware()
      .concat(blogsApi.middleware)
      .concat(singleBlogApi.middleware)
      .concat(createBlogApi.middleware)
      .concat(authApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;