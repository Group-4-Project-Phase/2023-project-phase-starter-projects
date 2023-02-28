export interface User {
  _id?: string;
  name: string;
  email: string;
  password: string;
  image?: string;
}

export interface Blog {
  _id?: string;
  image: string;
  title: string;
  description: string;
  author?: User;
  createdAt?: string;
  updatedAt?: string;
  skills: string[];
  tags: string[];
  isPending?: boolean;
  likes?: number;
  relatedBlogs?: Blog[];
  __v?: number;
}

export interface TokenAndUser {
  token: string
  user: {
    email:string,
    name:string
  } | null
}
