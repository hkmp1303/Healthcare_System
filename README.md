# Healthcare System
A console based program written in C# (.NET 9) where users manage patient healthcare data, administrative tasks and complete patient documentation.

The project was created in collaboration with other NBI students as a group assignment for learning purposes.

### Learning objectives
Familiarizing project members with version control strategies like using branches to support parallel feature development and code review to increase code quality were key skill development goals.

Utilizing github features such as creating millstones and issues to track goal progression and promote development of collaborative communication around workflow, problem identification and resolution were additional skill development goals.

Lastly, producing the minimal viable product (MVP) was emphasized as a key decision making condition when considering design options or feature expansions.

## ✔️ System requirements:
- [ ] security and safety: limit user access to information based on role
- **Users**
  - [X] login
  - [X] logout
  - [X] request registration as a patient
  - [X] once logged in: view my schedule
- **Admin user**
  - [ ] assign personnel to regions
  - [ ] add locations
  - [X] accept user registration as a patient
  - [X] deny user registration as a patient
  - [X] create personnel accounts
  - [X] view the permissions list
  - [X] assign permissions for the permission system in fine granularity including:
    - [X] permission to handle registrations
    - [X] permission to add locations
    - [X] permission to create personnel accounts
    - [X] permission to view the permissions list
- **Personnel user**
  - [X] view patients journal entries
  - [X] assign journal entries various levels of read permissions
  - [X] register appointments
  - [X] modify appointments
  - [X] approve appointment requests
  - [ ] view schedule by location
- **Patient user**
  - [X] view own journal
  - [X] request an appointment

### Future feature creeps
- [ ] implement dictionary for user accounts
- [ ] Approve/deny multiple registration requests without moving between menus
- [ ] Revoking assigned permission rights
- [ ] An option to revoke patient registration requests as a user

## 🚀 How to Run
```
git clone git@github.com:hkmp1303/Healthcare_System.git

cd Healthcare_System

dotnet run
```

## 🦮 Quick guide
Follow on screen console prompts throughout the system. Menus will display key characters for user selection to navigate the program. 

## 🖌️ Design Structure
The goals of this project include to practice using OOP, event based design and collaboration with multiple project contributors.

In accordance with these goals, as internal program states change these changes alter object behavior.

### Data
The project uses CSV files for persistent data storage between sessions. The CSV file updates when data changes occur and is saved when exiting the program.

### Project structure
![UML Diagram of system classes](docs/uml25.png)

Fullview of UML class diagram can be accessed [here](https://www.yworks.com/yed-live/?file=https://gist.githubusercontent.com/hkmp1303/ce4ffc1ec8558003ab33a2dafa426a9b/raw/53ffb886dc941c9a6d531614947ff905c4858f30/Healthcare%20System).
