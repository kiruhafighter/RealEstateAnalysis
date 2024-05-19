//BOOKSSLICE
export type Book = {
  key: string,
  id: string,
  image:string,
  title:string,
  author:string,
  description:string,
  price:number,
  category:string,
  };

  export enum Status {
    LOADING = 'loading',
    SUCCESS = 'success',
    ERROR = 'error',
  }
  
  export interface BookSliceState {
    items: Book[];
    status: Status;
  }

  //CARTSLICE
  export type CartItem = {
    id: string;
    title: string;
    price: number;
    image: string;
    author: string;
    count: number;
  };
  
  export interface CartSliceState {
    totalAmount:number;
    totalPrice:number;
    items: CartItem[],
  }

  //FORMSLICE
  export type ValuesItems = {
    firstName: string;
    lastName: string;
    billingCountry: string;
    billingAddress: string;
    billingPhoneNumber: string;
    checkAdress: boolean;
    deliveryCountry: string;
    deliveryAddress: string;
    deliveryPhoneNumber: string;
    paymentMethod: string;
    deliveryDate: string;
    observations: string;
    checkRecommend: boolean;
    orderNum: number;
  }
  
  export interface FormSliceState {
    values?: ValuesItems[];
  }

  //USERSLICE
  export interface UserSliceState {
    email: string,
    token: string,
    id: number,
    isLogin: boolean,
  }
  

