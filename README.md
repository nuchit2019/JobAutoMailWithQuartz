# JobAutoMailWithQuartz
Scheduling Background Jobs Automail With Quartz.NET
# 
## Project Summary
The `Janawat.EmailService` is a .NET 8 application utilizing **Quartz.NET** for job scheduling to automate email sending tasks. The application runs as a **Background Service** in a **Worker Service** project, leveraging **Dependency Injection (DI)** and **Hosted Services**. It primarily focuses on scheduling and sending automated emails using **MailKit** via SMTP.

## Class Summary

### 1. **Worker.cs**
- Manages the lifecycle of the **Quartz Scheduler** as a **Background Service**.

### 2. **JobFactory.cs**
- Custom **IJobFactory** implementation for DI and job instantiation.

### 3. **QuartzConfigurator.cs**
- Configures **Quartz.NET** triggers and job scheduling logic.

### 4. **EmailService.cs**
- Implements **IEmailService** to handle **SMTP Email Sending** using **MailKit**.

### 5. **SendEmailJob.cs**
- Defines the logic for the scheduled job to send emails.

### 6. **Program.cs**
- Application entry point, sets up **Dependency Injection**, **Quartz Services**, and **Hosted Services**.

## Tech Stack
- **.NET 8 Worker Service**
- **Quartz.NET** (Job Scheduling)
- **MailKit** (SMTP Email Sending)
- **Dependency Injection**
- **Hosted Service**
- **Cron Scheduling**
- **Microsoft.Extensions.Logging**

## Create Project:

1. **Create Worker Service**
```sh
dotnet new worker -n Janawat.EmailService
cd Janawat.EmailService
```

2. **Add Required Packages**
```sh
dotnet add package Quartz.Extensions.Hosting
```

3. **Create Project Structure**
```sh
mkdir -p Application/Jobs Domain/Interfaces Infrastructure/Services Worker/Quartz
```

4. **Add Required Classes**
- `Worker.cs`
- `JobFactory.cs`
- `QuartzConfigurator.cs`
- `EmailService.cs`
- `SendEmailJob.cs`
- `Program.cs`

5. **Configure SMTP in appsettings.json**
```json
"SmtpSettings": {
    "Host": "smtp.example.com",
    "Port": "587",
    "Username": "your-email@example.com",
    "Password": "your-password"
}
```

## Step-by-Step: Coding

1. **Setup Worker.cs** to initialize and start the **Quartz Scheduler**.

2. **Implement JobFactory.cs** to handle DI for **Quartz Jobs**.

3. **Create SendEmailJob.cs** to execute email sending logic.

4. **Implement EmailService.cs** using **MailKit** to send emails via **SMTP**.

5. **Configure Quartz Services** in **QuartzConfigurator.cs**, including **Cron Triggers**.

6. **Update Program.cs** to wire up **DI**, **Quartz**, and **Hosted Service**.

7. **Run Application**
```sh
dotnet build
dotnet run --environment Development
```

## Deployment
The worker service can be hosted in various environments such as **Windows Services**, **Linux daemons**, or **Docker Containers**.

## Contributing
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-feature`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to the branch (`git push origin feature/my-feature`)
5. Create a new **Pull Request**

## Contact

For questions or support, please contact:  
**Nuchit Atjanawat**  
**Email**: nuchit@outlook.com  
**GitHub**: [nuchit2019](https://github.com/nuchit2019)

# 

