# Setting Up RabbitMQ with Docker

For local development on Windows, the easiest and cleanest way to run RabbitMQ is using **Docker**. This avoids installing Erlang and RabbitMQ directly on your machine.

## 1. The Easy Way (Project-Integrated)
We have added RabbitMQ to the project's `docker-compose.yml`. You don't need to do anything special!

### Run the stack
```bash
docker-compose up -d
```

### Access Management Console
Once running, you can access the RabbitMQ Management Interface at:
- **URL**: [http://localhost:15672](http://localhost:15672)
- **Username**: `guest`
- **Password**: `guest`

## 2. The Manual Way (Standalone Container)
If you ever need to run it separately from the project stack:

```bash
docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

## Key Concept: Ports
- **5672**: The port your .NET applications use to send/receive messages (AMQP protocol).
- **15672**: The web-based management UI.

## Troubleshooting
- **Connection Refused**: Ensure the container is running (`docker ps`).
- **Access Denied**: The default `guest`/`guest` credentials only work from `localhost`.
