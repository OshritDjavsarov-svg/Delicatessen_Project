
export interface Product {
    productId?: number;
    categoryName: string;
    productName: string;
    imageUrl: string;
    productDescription: string;
    productPrice: number;
    priceDescription?: string;
    heatingInstruction?: string;
    allergens?: string;
    ingredients?: string;
    nutritionalValues?: string;
    filterProduct: string;
    highQuantity?: number[];
}
