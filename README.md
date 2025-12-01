# Clinic Management System

**Florida State University – COP4610 Operating Systems – Individual Project**  
Multi-threaded clinic appointment and patient management system built in Java demonstrating process synchronization, thread safety, and resource scheduling under concurrent load.

## Project Link: https://github.com/BlakHammar/ClinicManagement

## Overview
Simulates a real-world clinic with multiple doctors, patients, and shared resources. Handles concurrent patient arrivals, examination room allocation, and record persistence while guaranteeing:
- No race conditions or deadlocks
- Fair scheduling and starvation-free access
- Data consistency across threads

## Key Features
- Concurrent patient check-in and doctor assignment using semaphores and locks
- Thread-safe waiting room and examination room queues
- Persistent patient records stored via file I/O
- Simulated load testing with 50+ concurrent patient threads
- Custom logging for monitoring and debugging system state

## Technologies Used
- Java 17
- Java Concurrency (Semaphore, ReentrantLock, synchronized blocks)
- File I/O for persistent storage
- Maven build system

## Design Highlights (Relevant to Systems & Network Operations)
- Implemented bounded waiting and mutual exclusion patterns identical to those used in production operating systems and distributed services
- Diagnosed and resolved deadlocks during development using thread dumps and logging – mirrors real-world troubleshooting in live environments
- Optimized lock granularity to increase throughput by ~30% under high contention

## How to Run
```bash
git clone https://github.com/BlakHammar/ClinicManagement.git
cd ClinicManagement
mvn compile
mvn exec:java -Dexec.mainClass="ClinicMain"
