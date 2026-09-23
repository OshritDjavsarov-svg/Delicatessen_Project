export interface User {
    customerId?: number;            // ה-? אומר שהשדה אופציונלי (כי בהרשמה עוד אין ID)
    privateName: string;
    lastName: string;
    email: string;
    password?: string;     
    phone: string;
    city: string;
    address: string;       
    houseNumber?: number;
    entry?: number;
    apartment?: number;
    floor?: number;
    
}