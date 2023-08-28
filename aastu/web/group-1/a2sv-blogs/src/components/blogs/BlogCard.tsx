"use client";
import React, { useState } from "react";
import Image from "next/image";
import { useGetBlogsQuery } from "@/lib/redux/features/blog";
import Link from "next/link";

export default function BlogCard() {
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 3;
  const { data, isLoading, error } = useGetBlogsQuery();

  const totalItems = data?.length ?? 0;
  const totalPages = Math.ceil(totalItems / pageSize);

  const startIndex = (currentPage - 1) * pageSize;
  const endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);
  const currentItems = data?.slice(startIndex, endIndex + 1);

  const options = { year: "numeric", month: "short", day: "2-digit" };
  console.log(data);
  return (
    <>
      {data?.map((item, index) => (
        <div key={index} className="w-full h-80  border-t border-[#D7D7D7]">
          <div className="h-[25%] flex items-center gap-3 px-4">
            <Image
              className="w-14 h-14 rounded-full object-cover"
              src={item.image}
              alt=""
              width={100}
              height={100}
            />
            <div className="flex flex-col gap-0.5">
              <h1 className="font-montserrat font-semibold text-base leading-5 flex gap-0">
                {item.author?.name}
                <span className="flex items-center">
                  <svg
                    className="w-5 h-5 shrink-0"
                    viewBox="0 0 25 25"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <circle
                      cx="12.5"
                      cy="12.5"
                      r="1.5"
                      fill="#121923"
                      stroke="#121923"
                      stroke-width="1.2"
                    />
                  </svg>
                </span>
                <span className="text-xs flex items-center text-[#868686]">
                  {new Date(item.updatedAt).toLocaleDateString(
                    "en-US",
                    options
                  )}
                </span>
              </h1>
              <p className="font-montserrat font-semibold tracking-wide text-xs text-[#9c9c9c]">
                {item.author?.role.toUpperCase()}
              </p>
            </div>
          </div>
          <div className="h-[60%] flex items-center mx-auto px-4 gap-14 py-5">
            <div className="flex flex-col">
              <h1 className="font-montserrat text-xl font-black leading-9 text-black">
                {item.title}
              </h1>
              <p className="font-montserrat text-base font-normal leading-7 text-[#737373]">
                {item.description}
              </p>
            </div>
            <Image
              src={item.image}
              alt=""
              width={300}
              height={200}
              className="w-72 h-48 rounded-xl object-cover"
            />
          </div>
          <div className="flex items-center">
            {item?.tags?.map((tag, index) => (
              <ul
                className="h-[15%] flex items-center gap-10 px-3 justify-start"
                key={index}
              >
                <li className=" px-5 py-1.5 font-montserrat font-semibold text-sm text-[#8E8E8E] bg-[#ededf0] rounded-full flex">
                  {tag}
                </li>
              </ul>
            ))}
          </div>
        </div>
      ))}

      <div className="flex justify-center mb-10">
        <div className="flex gap-5">{renderPageButtons()}</div>
      </div>
    </>
  );
}
