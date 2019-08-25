# Brook.CnblogImage.Core
> 博客园图床api 🤣👻
```
docker-compose up -d
```
* 开箱即食
```
version: '3.4'

services:
  brook.cnblogimage.core:
    image: gaozengzhi/brook-cnblog-core
    ports:
      - "32769:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_CNBLOG_BLOGID=xxx
      - ASPNETCORE_CNBLOG_USERNAME=xxx
      - ASPNETCORE_CNBLOG_PASSWORD=xxx
      - ASPNETCORE_TOKEN=xxxx
      - VIRTUAL_HOST=xxx.xxx.cn
    restart: always 
    volumes:
      - ./Resources:/app/Resources

networks:
   default:
     external:
       name: nginx-proxy
```

* dockerfile编译
```
version: '3.4'

services:
  brook.cnblogimage.core:
    image: ${DOCKER_REGISTRY-}brookcnblogimage
    build:
      context: .
      dockerfile: Brook.CnblogImage.Core/Dockerfile
    ports:
      - "32769:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_CNBLOG_BLOGID=xxxx
      - ASPNETCORE_CNBLOG_USERNAME=xxxx
      - ASPNETCORE_CNBLOG_PASSWORD=xxx
      - ASPNETCORE_TOKEN=xxxx
    restart: always 
    volumes:
      - ./Resources:/app/Resources
```

* 如何使用
![](https://github.com/yuefengkai/Brook.CnblogImage.Core/blob/master/assets/2.jpeg?raw=true)
![](https://github.com/yuefengkai/Brook.CnblogImage.Core/blob/master/assets/1.png?raw=true)
-------

如果觉得有用请给我个Start!
> 作者：Brook（高增智）

* 参考
> [如何高效的编写与同步博客 （.NET Core 小工具实现）](https://www.cnblogs.com/stulzq/p/9043632.html)
