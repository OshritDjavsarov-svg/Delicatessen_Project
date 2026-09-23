using AutoMapper;
using BLL.BLLInterfaces;
using DAL.DALInterfaces;
using DAL.DALServises;
using DAL.models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BLLServises
{
    public class UserBLLServise: IUserBLLService
    {
        IUserDALServise userdALServise;
        private readonly IMapper _mapper;
        public UserBLLServise(IUserDALServise userdALServise, IMapper mapper)
        {
            this.userdALServise = userdALServise;
            _mapper = mapper;
        }

        // פונקצית עזר לסיסמא
        private string HashPassword(string password)
        {
            // ברירת מחדל ל-Work Factor (10) מספיקה לרוב היישומים
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        private bool VerifyPassword(string password, string hash)
        {
            // משווה את הסיסמה שהוזנה מול ה-Hash השמור במסד הנתונים
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        public UserResponseDto Register(UserRegisterDto userDto)
        {
            // 1. לוגיקה: בדיקה אם האימייל כבר קיים במערכת
            if (userdALServise.GetCustomerByEmail(userDto.Email) != null)
            {
                // על ידי כך מובן שההרשמה לא הצליחה כי קיים כבר מייל זהה במערכת
                return null;
            }

            // 2. מיפוי מה-DTO שקיבלנו ל-Entity ששומרים בבסיס הנתונים
            Customer customerEntity = _mapper.Map<Customer>(userDto);

            // מיפוי הסיסמא
            customerEntity.PasswordHash = HashPassword(userDto.Password);

            // 3. שמירת ה-Entity ב-DAL
            Customer newCustomer = userdALServise.AddCustomer(customerEntity);

            // 4. מיפוי מה-Entity שנוצר (כולל ה-CustomerId החדש) ל-UserResponseDto
            UserResponseDto response = _mapper.Map<UserResponseDto>(newCustomer);

            return response;
        }

        public UserResponseDto Login(UserLoginDto loginDto)
        {
            // 1. שלב ראשון: שליפת המשתמש לפי האימייל (לפני בדיקת סיסמה)
            // אנו צריכים את ה-Hash השמור במסד הנתונים כדי לבדוק אותו מול הסיסמה שהוזנה.
            Customer customerEntity = userdALServise.GetCustomerByEmail(loginDto.Email);

            // 2. לוגיקה: בדיקה אם המשתמש נמצא
            if (customerEntity == null)
            {
                // משתמש לא קיים (נחזיר null כדי לא לחשוף האם המייל קיים)
                return null;
            }

            // 3. **************** לוגיקה קריטית: אימות סיסמה ****************
            // נשתמש ב-VerifyPassword כדי להשוות את הסיסמה שהוזנה מול ה-Hash השמור
            bool isPasswordValid = VerifyPassword(loginDto.Password, customerEntity.PasswordHash);

            if (!isPasswordValid)
            {
                // סיסמה לא נכונה
                return null;
            }


            // 4. הצלחה: המרת ה-Entity ל-UserResponseDto והחזרה
            UserResponseDto response = _mapper.Map<UserResponseDto>(customerEntity);
            return response;
        }
    }
}
