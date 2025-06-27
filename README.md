# Real Estate Microservices System

An in-progress, secure, and scalable real estate platform being developed using Angular and .NET 8 microservices with Clean Architecture. This monorepo contains 12 independently designed services that are being developed to work together as a complete system.

---

## 🧱 Projects Overview (12 Total)

### 🔐 1. AuthService
- Handles user authentication, registration, and service validation

### 🏠 2. PropertyService
- Manages properties and their units

### 👤 3. TenantService
- Manages tenant profiles and identity

### 📅 4. BookingService
- Handles unit bookings by tenants

### 💳 5. PaymentService
- Will handle booking payment processing

### 🖼️ 6. FileService
- For image upload and management for properties and units

### 📄 7. ReportingService
- Will generate PDF reports upon successful bookings

### 📧 8. EmailService
- For sending email confirmations and notifications

### 📲 9. SmsService
- For sending SMS notifications to users

### 🔀 10. ApiGateway
- Ocelot-based gateway for routing frontend requests to backend services

### 🌐 11. UI (Angular Frontend)
- Angular 17 application for tenants and managers

### 📦 12. SharedKernel
- Contains shared abstractions, DTOs, and base classes used across services

---

## 📁 Folder Structure
```
RealEstateMicroservices/
├── AuthService/
├── PropertyService/
├── TenantService/
├── BookingService/
├── PaymentService/
├── FileService/
├── ReportingService/
├── EmailService/
├── SmsService/
├── ApiGateway/
├── UI/
│   └── RealEstateApp/  // Angular frontend
├── SharedKernel/
├── docker-compose.yml
├── .gitignore
└── README.md
```

---

## 🛠️ Technologies Being Used
- **Backend:** ASP.NET Core 8 (Web API, Clean Architecture)
- **Frontend:** Angular 17
- **Messaging:** RabbitMQ / Azure Service Bus (planned)
- **PDF:** QuestPDF / DinkToPDF (planned)
- **File Storage:** Azure Blob / Local (planned)
- **Email:** SendGrid / SMTP (planned)
- **SMS:** Twilio / Africa's Talking (planned)
- **Gateway:** Ocelot
- **Auth:** JWT-based, service-to-service validation

---

## 🚀 How to Run (when implemented)
1. Clone the repository:
```bash
git clone https://github.com/GilbertKibet/RealEstateMicroservices.git
```

2. Navigate to project folders and run each service or use:
```bash
docker-compose up --build
```

3. UI will be accessible at: `http://localhost:4200`
4. Gateway routes will be available via Ocelot at: `http://localhost:7000`

---

## 📌 Development Roadmap
- [ ] Scaffold all services and UI
- [ ] Set up service registration and communication via AuthAPI
- [ ] Secure API Gateway with Ocelot and JWT validation
- [ ] Add Docker support and CI/CD pipelines
- [ ] Integrate RabbitMQ, Email, and SMS functionality
- [ ] Finalize UI and complete end-to-end booking flow

---

## 👨‍💻 Maintainer
**Gilbert Kibet**  
Software Engineer 
🔗 [LinkedIn](https://www.linkedin.com/in/gilbert-kibet-ba494519b/)
