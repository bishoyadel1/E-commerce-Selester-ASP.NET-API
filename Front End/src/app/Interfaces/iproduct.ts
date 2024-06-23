import { Iwarranty } from "./iwarranty";

export interface Iproduct {
  id: number;
  name: string;
  description: string;
  price: number;
  discount: number;
  priceAfter: number;
  condition: number;
  stockQuantity: number; // want in admin
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
  categoryName: string;
  brandID: number;
  brandName: string;
  warranties: Iwarranty[];  // array of object (Iwarranty)
  images: string[];         // array of strings just for now
  avgRating: number;
  avgRatingRounded: number;
}
