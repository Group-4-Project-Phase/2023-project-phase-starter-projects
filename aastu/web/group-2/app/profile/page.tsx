'use client';
import { useEditUserMutation } from '@/lib/redux/slices/usersApi';
import Image from 'next/image';
import { useEffect, useState } from 'react';
import React, { useCallback } from 'react';
import { useDropzone } from 'react-dropzone';
import uploadIcon from '@/assets/images/Group 9542.svg';

export default function Section() {
  const [profileImage, setProfileImage] = useState<File>();

  const createFile = async (url: string) => {
    let response = await fetch(url);
    let data = await response.blob();
    let metadata = {
      type: 'image/jpeg',
    };
    let file = new File([data], 'test.jpg', metadata);
    // ... do something with the file or return it
    setProfileImage(file);
  };

  const data = localStorage.getItem('login');
  console.log(data);
  let user = null;
  if (data) {
    user = JSON.parse(data);
  }
  useEffect(() => {
    createFile(user.userProfile);
  }, []);
  const [editUser] = useEditUserMutation();

  const [first, setFirst] = useState(user.userName.split(' ')[0]);
  const [second, setSecond] = useState(user.userName.split(' ')[1]);
  const [email, setEmail] = useState(user.userEmail);

  console.log(user.userProfile);
  const [imgPath, setImgPath] = useState<any>(user.userProfile || '');

  const onDrop = useCallback((acceptedFiles: File[]) => {
    // Do something with the files
    setImgPath(URL.createObjectURL(acceptedFiles[0]));
    setProfileImage(acceptedFiles[0]);
  }, []);

  const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop });

  const handleEditUser = async () => {
    // e.preventDefault()
    console.log(email, profileImage);
    const form = new FormData();
    form.append('email', email);
    form.append('name', first + ' ' + second);
    if (profileImage) {
      form.append('image', profileImage);
    }
    try {
      const res = await editUser(form).unwrap();
      // console.log("worked", res)
      const userData = JSON.parse(localStorage.getItem('login') || '');
      // console.log("upated11", userData)
      const obj = { ...userData };
      obj.userEmail = res.body.email;
      obj.userProfile = res.body.image;
      obj.userName = res.body.name;
      console.log('upated', obj);
      localStorage.removeItem('login');
      localStorage.setItem('login', JSON.stringify(obj));
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="space-y-4">
      <div className="flex flex-col items-center gap-4 md:flex-row md:justify-between py-2 border-b-2 border-[#EFEFEF] mb-7 px-4">
        <div className=" font-secondaryFont">
          <h3 className="font-semibold text-textColor-200 text-lg text-center md:text-left md:text-lg">
            Manage Personal Information
          </h3>
          <p className="font-medium text-textColor-50 text-base">
            Add all the required information about yourself
          </p>
        </div>
        <div className=" flex items-center">
          <button
            onClick={() => handleEditUser()}
            className="bg-primaryColor text-white font-secondaryFont font-semibold rounded-lg px-8 py-2"
          >
            Save Changes
          </button>
        </div>
      </div>

      <form className="space-y-6" action="">
        <div className="space-x-8 flex border-b-[1px] py-6">
          <label className="mr-16 font-semibold" htmlFor="name">
            Name
          </label>
          <div>
            <input
              type="text"
              name="name"
              id="name"
              defaultValue={first}
              onChange={(e) => setFirst(e.target.value)}
              className=" border border-[#E4E4E4] rounded-md p-2 mr-12 mb-6"
            />
            <input
              type="text"
              name="fname"
              id="fname"
              defaultValue={second}
              onChange={(e) => setSecond(e.target.value)}
              className=" border border-[#E4E4E4] rounded-md p-2"
            />
          </div>
        </div>
        <div className="space-x-8 border-b-[1px] py-6">
          <label className="mr-16 font-semibold" htmlFor="email">
            Email
          </label>
          <input
            type="text"
            name="email"
            id="email"
            defaultValue={email}
            onChange={(e) => setEmail(e.target.value)}
            className=" border border-[#E4E4E4] rounded-md p-2 mb-6"
          />
        </div>
        <div className="space-x-8 flex border-b-[1px] py-6">
          <label className="mr-16 font-semibold" htmlFor="picture">
            Your Photo
          </label>
          <Image
            src={imgPath}
            alt={'profile'}
            width={100}
            height={100}
            className="inline-block rounded-full h-24 self-center w-24"
          />

          {/* name="picture"
                        id="picture"
                        placeholder="Enter your picture"
                        className=" border border-[#E4E4E4] rounded-md p-2 h-40" /> */}
          <div {...getRootProps()}>
            <input {...getInputProps()} />
            {isDragActive ? (
              <p>Drop the files here ...</p>
            ) : (
              <p>Drag 'n' drop some files here, or click to select files</p>
            )}
          </div>
        </div>
      </form>
    </div>
  );
}
