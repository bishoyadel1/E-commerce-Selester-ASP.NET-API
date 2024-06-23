import { Iwarranty } from "../iwarranty";

export interface Iproductadd {
  name: string;
  description: string;
  price: number;
  condition: number;
  stockQuantity: number;
  discount: number;
  model: string;
  color: string;
  productDetailone: string;
  productDetailtwo: string;
  productDetailthree: string;
  productDetailfour: string;
  productDetailfive: string;
  productDetailsix: string;
  productDetailsiven: string;
  categoryID: number;
  brandID: number;
  warranties: Iwarranty[];
  images: File[];
}


