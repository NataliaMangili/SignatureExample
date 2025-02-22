//worker_processes 1;

//events {
//    worker_connections 1024;
//}

//http {
//    include       mime.types;
//    default_type  application/octet-stream;

//    sendfile        on;
//    keepalive_timeout 65;

//     # Nome do container e porta

//    upstream delivery_service {
//        server deliveryservice:5001;  
//    }

//    upstream payment_service {
//        server paymentservice:5002;  
//    }

//    upstream log_service {
//        server logservice:5003; 
//    }

//    upstream email_service {
//        server emailservice:5004; 
//    }

//    server {
//        listen 80;

//        # Rota para o Service
//        location /api/delivery {
//            proxy_pass http://delivery_service;
//        }

//        location /api/payment {
//            proxy_pass http://payment_service;
//        }

//        location /api/logs {
//            proxy_pass http://log_service;
//        }

//        location /api/email {
//            proxy_pass http://email_service;
//        }
//    }
//}