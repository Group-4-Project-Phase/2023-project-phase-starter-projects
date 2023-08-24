'use client'
import React, { useEffect, useState } from 'react'
import { useParams } from 'next/navigation';
import Image from 'next/image';
import SingleBlog from '@/app/components/blog/singleBlog';

interface Blog {
  _id: string;
  image: string;
  title: string;
  description: string;
  author: null | Author; 
  isPending: boolean;
  tags: string[];
  likes: number;
  relatedBlogs: Blog[];
  skills: string[];
  createdAt: string;
  updatedAt: string;
  __v: number;
}

interface Author {
  _id: string;
  name: string;
  email: string;
  image: string;
  role: string;
}

const First = () => {
  const router = useParams();
  const { id } = router;
  const [blogInfo, setBlogInfo] = useState<Blog>();
  const [blogs, setBlogs] = useState([]);
  
  useEffect(() => {
    fetch('https://a2sv-backend.onrender.com/api/blogs/')
      .then((response) => response.json())
      .then((data) => {
        const filteredBlogs = data.filter((blog: any) => blog.id !== id);
        setBlogs(filteredBlogs);
      });
  }, []);
  console.log(blogs);
  
  useEffect(()=>{
        fetch(`https://a2sv-backend.onrender.com/api/blogs/${id}`).then((res)=>{

          return res.json()
        }).then((data)=>{
          console.log(data)
          setBlogInfo(data)
        })
    }, [id])
    
    console.log(blogInfo)
  
  let authorName = blogInfo?blogInfo.author?blogInfo.author.name:"":""
  authorName = authorName.replace(" ","_")
  console.log(authorName)

  return (
    <div className='grid grid-rows gap-0'>
    <div className='text-3xl font-primaryFont text-center mt-20 mx-auto'>{blogInfo?.title}</div>
    <div className='text-xs font-secondaryFont flex items-center justify-center font-light uppercase text-gray-500 h-5 m-3'>
     {blogInfo?.tags.map((tag) => {
        return tag;
      }).join(", ")}
      &nbsp;&nbsp;|&nbsp;&nbsp;6 min read</div>
    <div className='flex items-center justify-center m-9'>
    <Image
      src={blogInfo?blogInfo.image:""}
      width={980}
      height={514.5}
      alt="Picture"
      className='w-full max-w-screen-xl mx-auto'
    /></div>
    {blogInfo?.author && (
      <div>
        <div className='flex items-center justify-center'>
        <Image
        src={blogInfo?blogInfo.author?blogInfo.author.image:"":""}
        width={80}
        height={80}
        alt="Picture of the author"
        className='rounded-full'
      /></div>
      <div className='text-xs font-secondaryFont flex items-center justify-center font-normal uppercase h-5 mt-3'>{blogInfo?blogInfo.author?blogInfo.author.name:"":""} &nbsp;&nbsp;| &nbsp;&nbsp; software engineer</div>
      <div className='text-xs font-secondaryFont flex items-center justify-center font-semibold text-blue-600 uppercase h-5'>@{authorName}</div>
      </div>
        )}
    <div className='text-xl lg:text-2xl font-primaryFont flex items-center justify-center mx-4 lg:mx-64 mb-4 lg:mb-8 mt-8 pl-2'>{blogInfo?.description}</div>
    <div className='text-base lg:text-lg font-primaryFont flex flex-col items-center text-gray-600 justify-center mx-4 lg:mx-64 pl-2'>
        <p className='mb-4'>Lorem ipsum dolor sit amet consectetur adipisicing elit. Minus nemo aspernatur, est tempore facilis assumenda? Soluta itaque deserunt veniam ut nemo, culpa fugiat odit! Fugiat consequatur sed minima totam perspiciatis.</p>
        <p className='mb-4'>Lorem ipsum dolor sit amet consectetur adipisicing elit. Minus nemo aspernatur, est tempore facilis assumenda? Soluta itaque deserunt veniam ut nemo, culpa fugiat odit! Fugiat consequatur sed minima totam perspiciatis.</p>
        <p className='mb-4'>Lorem ipsum dolor sit amet consectetur adipisicing elit. Minus nemo aspernatur, est tempore facilis assumenda? Soluta itaque deserunt veniam ut nemo, culpa fugiat odit! Fugiat consequatur sed minima totam perspiciatis.</p>
        <p className='mb-4'>Lorem ipsum dolor sit amet consectetur adipisicing elit. Minus nemo aspernatur, est tempore facilis assumenda? Soluta itaque deserunt veniam ut nemo, culpa fugiat odit! Fugiat consequatur sed minima totam perspiciatis.</p>
    </div>
    <div className='text-xl font-secondaryFont font-semibold mt-20 mb-4 text-center lg:text-left lg:mx-48'>Related Blogs</div>
    <div className='flex flex-col lg:flex-row mx-auto lg:pl-40 mb-32'>
    
      {blogs.slice(0, 3).map((blog: any) => {
        console.log(blog.skills);
        return (
          <SingleBlog key={blog._id} 
          id={blog._id} image={blog.image} 
          authorName={blog?blog.author?blog.author.name:"":""} 
          title={blog.title}
          tags = {blog.tags}
          descrip = {blog.description}
          likes = {blog.likes}
          profilePic = {blogInfo?blogInfo.author?blogInfo.author.image:"":""}/>
        );
      })}
    </div>
  </div>
  )
} 


export default First
