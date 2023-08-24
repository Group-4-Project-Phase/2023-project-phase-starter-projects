'use client'

import { Provider } from "react-redux";
import "./globals.css";
import type { Metadata } from "next";
import store from "@/store";
import Footer from "@/components/footer";

export const metadata: Metadata = {
  title: "Create Next App",
  description: "Generated by create next app",
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
	return (
		<Provider store={store}>
			<html lang="en">
				<body>
					{children}
					<Footer/>
				</body>
			</html>
		</Provider>
	);
}
