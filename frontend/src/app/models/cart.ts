
export interface OrderItem {
    orderItemId: number;
    orderId: number;
    productId: number;
    quantity: number;
    orderItemPrice: number;
    product?: any; 
}

export interface Order {
    orderId: number;
    customerId: number;
    totalPrice: number;
    status: string;
    createdAt?: string; // תאריך מגיע כ-string ב-JSON
    orderItems: OrderItem[];
}