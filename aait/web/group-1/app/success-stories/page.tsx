import React from 'react';
import Image from 'next/image';
import Link from 'next/link';
import Story from "@/components/success-stories/Story";
import { SuccessStory } from "@/types/story";

export default async function Page() {
  const response = await fetch('https://a2sv-backend.onrender.com/api/success-stories');
  const allData: SuccessStory[] = await response.json();

  const partners: string[] = [
    'Google',
    'Palantir',
    'InstaDeep',
    'Meta',
    'Databricks',
    'Linkedin'
  ].map(partner => `/images/partners/${ partner }.png`);

  const socialIcons = [
    'facebook',
    'linkedin',
    'telegram',
    'twitter',
  ].map(icon => `/images/success-social-icons/${ icon }.png`);

  return (
    <div className='flex flex-col px-[100px] space-y-32'>

      <div className='text-center font-poppins mb-[60px]'>
        <h2 className='text-[35px] font-semibold mb-[25px]'>Impact Stories</h2>
        <p className='text-[25px] px-[270px]'>Behind every success is a story. Learn about the stories of A2SVians</p>
        <span></span>
      </div>

      <div className='flex flex-col space-y-20'>
        {
          allData.map(data => <Story data={ data }/>)
        }
      </div>
      <div>
        <h1 className='text-center text-4xl text-[#363636] font-dmSans mb-20'>Current Interview Partners</h1>
        <div className='flex justify-center items-center mt-5'>
          {
            partners.slice(0, 4).map(partner => {
              return (
                <Image
                  src={ partner }
                  width={ 250 }
                  height={ 60 }
                  alt={ partner }
                />
              )
            })
          }
        </div>
        <div className='flex justify-evenly items-center px-[400px]'>
          {
            partners.slice(4).map(partner => {
              return (
                <Image
                  src={ partner }
                  width={ 250 }
                  height={ 60 }
                  alt={ partner }
                />
              )
            })
          }
        </div>
      </div>
    </div>
  )
}
