import { v4 as uuidv4 } from 'uuid';

export interface IBasket {
  id: string;
  basketItems: IBasketItem[];
}

export interface IBasketItem {
  id: number;
  productName: string;
  brand: string;
  type: string;
  quantity: number;
  price: number;
  pictureUrl: string;
}

export class Basket implements IBasket {
  basketItems: IBasketItem[] = [];
  id: string = uuidv4();
}

export interface IBasketTotals {
  shipping: number;
  subTotal: number;
  total: number;
}
