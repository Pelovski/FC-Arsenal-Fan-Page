# Arsenal Fan Page

The Arsenal Fan Page is an interactive platform dedicated to fans of Arsenal Football Club, offering news, merchandise, and community engagement. The project is **deployed on Azure**, ensuring high availability and scalability. You can explore the platform [HERE](https://arsenalfanpage.azurewebsites.net) or use the following administrator credentials:

**Admin username**: `adminArsenal@fcarsenalfanpage.com`  
**Admin password**: `Admin2024!1`

**Product manager username**: `productManager@arsenalfanpage.com`  
**Product manager password**: `ProductManager!1`

**Content manager username**: `contentmanager@arsenalfanpage.com`  
**Content manager password**: `ContentManager!1`

## Roles and Permissions

### Administrator
- Full control over the site, including adding, editing, and deleting news and products.
- Can assign roles and has access to the entire admin panel.
- Ability to promote users to administrators.

### Assistant Administrator
- Has all the permissions of the Administrator, except promoting users to administrators.

### Content Creator
- Manages the news section. This user can create, edit, and delete news articles.

### Merchandising Specialist
- Handles the product section, with the ability to add, edit, and delete merchandise.

### User
- Standard user role. Users can read and comment on news, purchase fan merchandise, and manage their personal profiles, including profile pictures and delivery information.
- Users can also track their order history, viewing details about purchased items and their total value.
- Users can view the latest updates on Premier League standings, Champions League results, and upcoming Arsenal matches without needing to register.

## Features

- **Public Access**: All news, products, and live data (such as Premier League standings, Champions League results, and Arsenal match schedules) are accessible to everyone, even without registration.
- **Web API Integration**: The platform fetches real-time data from external APIs, ensuring that the latest football-related data, such as match results and upcoming fixtures, are always up-to-date and displayed for users to view.
- **Registration Requirement**: Users need to register to comment on news or purchase merchandise. Registration can be done manually or via social media (Facebook, Google, Twitter).
- **User Profiles**: Each user has a profile where personal details, delivery addresses, and profile pictures are stored.
- **Order Tracking**: Users can access their order history, viewing details about their past purchases.

## Technologies Used

- **ASP.NET Core**: Provides a robust backend framework.
- **MySQL**: Used for managing user data, news, and products.
- **Entity Framework Core**: Utilized for efficient data access and management of user, news, and product information in the application.
- **Web API**: Integrated for fetching live football data such as Premier League standings, Champions League results, and Arsenal match information.
- **JavaScript, HTML, CSS**: Ensures smooth frontend interactions and design.
- **Azure**: Deployed on Azure for scalability and reliability.
- **Unit Testing**: Implemented to ensure the reliability and correctness of the application.

## Project Summary

This project offers a comprehensive fan experience for Arsenal supporters, balancing news, products, and user interaction. The structure allows a clear division of roles, ensuring that users have access to appropriate features based on their responsibilities. The platform is designed following Object-Oriented Programming (OOP) principles and the Model-View-Controller (MVC) design pattern, catering to both casual users and administrators managing the site. It also provides a user-friendly interface with social media integration for easy registration and engagement, as well as real-time football data to keep fans up-to-date with the latest Arsenal-related developments.
