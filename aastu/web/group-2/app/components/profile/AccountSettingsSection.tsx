import React, { useState } from 'react'

const AccountSettingsSection = () => {

  const [currentPassword, setCurrentPassword] = useState("")
  const [newPassword, setNewPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")

  const handleSaveChange = (e: any) => {
    e.preventDefault()

    if (!currentPassword.length || !newPassword.length || !confirmPassword.length) {
      alert("All fields must be filled!")
    } else {

    }
  }

  return (
    <section>
      <div className='flex flex-col items-center gap-4 md:flex-row md:justify-between py-2 border-b-2 border-[#EFEFEF] mb-7 px-4'>
        <div className=' font-secondaryFont'>
          <h3 className='font-semibold text-textColor-200 text-lg text-center md:text-left md:text-lg'>Manage Your Account</h3>
          <p className='font-medium text-textColor-50 text-base'>You can change your password here</p>
        </div>
        <div className=' flex items-center'>
          <button onClick={handleSaveChange} className='bg-primaryColor text-white font-secondaryFont font-semibold rounded-lg px-8 py-2'>Save Changes</button>
        </div>
      </div>
      <div>
        <form className='flex flex-col items-center gap-y-6 mt-16 md:mb-96 mb-20'>
          <div className='flex flex-col items-start md:flex-row md:justify-between md:items-center md:gap-x-7 gap-y-3'>
            <label htmlFor="currentPassword" className='font-semibold text-lg text-textColor-200'>Current Password</label>
            <input
              type="password"
              name='currentPassword'
              id='currentPassword'
              placeholder='Enter your current password'
              value={currentPassword}
              onChange={(e) => setCurrentPassword(e.target.value)}
              className='w-96 md:w-80 px-4 py-3 rounded-md md:ml-1'
            />
          </div>
          <div className='flex flex-col items-start md:flex-row md:justify-between md:items-center md:gap-x-7 gap-y-3'>
            <label htmlFor="newPassword" className='font-semibold text-lg text-textColor-200 md:mr-5'>New Password</label>
            <input
              type="password"
              name='newPassword'
              id='newPassword'
              placeholder='Enter new password'
              value={newPassword}
              onChange={(e) => setNewPassword(e.target.value)}
              className='w-96 md:w-80 px-4 py-3 rounded-md md:ml-2'
            />
          </div>
          <div className='flex flex-col items-start md:flex-row md:justify-between md:items-center md:gap-x-7 gap-y-3'>
            <label htmlFor="confirmPassword" className='font-semibold text-lg text-textColor-200'>Confirm Password</label>
            <input
              type="password"
              name='confirmPassword'
              id='confirmPassword'
              placeholder='Confirm new password'
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              className='w-96 md:w-80 px-4 py-3 rounded-md '
            />
          </div>
        </form>
      </div>
    </section>
  )
}

export default AccountSettingsSection