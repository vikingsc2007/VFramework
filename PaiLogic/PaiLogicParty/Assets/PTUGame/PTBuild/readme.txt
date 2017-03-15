PTBuilder 版本：version 2.1.3
-----------------2017-3-6  xiaojun@putao.com--------------
version 2.1.3
1.增加android打包设置icon的功能
2.将IOS打测试包名称由“Build_XCode_Firim”改为“Build_XCode_Test”

-----------------2017-1-6  wanzhenyu@putao.com--------------
version 2.1.2
1.添加复制streamingassets 到 xcode raw 

-----------------2017-1-5  wanzhenyu@putao.com--------------
version 2.1.1
1.测试服 开发服 打开  allow transport security ，线上版本关闭

-----------------2017-1-3  wanzhenyu@putao.com--------------
version 2.1.0
1.iOS上去除APS allow transport security

-----------------2016-12-20  xiaojun@putao.com--------------
version 2.0.9
1.修改apk文件名

-----------------2016-12-19  xiaojun@putao.com--------------
version 2.0.8
1.加入自动签名部分

-----------------2016-12-19  xiaojun@putao.com--------------
version 2.0.7
1.添加Android打包机制

-----------------2016-08-31  wanzhenyu@putao.com--------------
version 2.0.6
1.修复自动设置autoscriptbackendscript。
2.添加自动设置为ios minimum os version 7.0

-----------------2016-08-08  wanzhenyu@putao.com--------------
version 2.0.5
1.修复多次处理 ptgame_projmods 文件，导致 embed_binaries 多条。

-----------------2016-08-03  wanzhenyu@putao.com--------------
version 2.0.4
1.添加测试服，开发服，线上服区分

-----------------2016-07-19  wanzhenyu@putao.com--------------
version 2.0.3
1.修改生成的python 打包脚本，改为相对路径。
2.修复远程打包。上次huangguichen@putao.com切换为python后，没有处理远程打包的情况。

notes:
如果shell脚本运行出错。可能是编码问题。请在mac电脑上打开相关类文件。然后点击转换保存。
-----------------2016-07-06  wanzhenyu@putao.com--------------
version 2.0.2
1.添加autosetscriptbackend 项，用来控制是否使用il2cpp编译 


-----------------2016-06-16  wanzhenyu@putao.com--------------
version 2.0.1
1.增加打包前预处理接口。需要在PTGameData/Editor/下面添加PTCustomPreBuild .cs
   文件下载地址：http://10.172.98.120:8888/ptplugin_unity/ref/PTCustomPreBuild.cs
2.修改证书配置文件读取路径。需要在PTGameData/Editor/config_ptgame/下面添加 config_certificate.xml
   文件下载地址：http://10.172.98.120:8888/ptplugin_unity/ref/config_certificate.xml
3.ptconfig_inner.xml ptconfig_online.xml 中增加了gpush部分 
   参考文件下载地址：http://10.172.98.120:8888/ptplugin_unity/ref/ptconfig_inner.xml


-----------------2016-06-14  huangguichen@putao.com--------------
version 2.0.0
1.重构打包操作 由shell打包改为python打包
2.包名 证书等配置放入process_codesign.xml中独立配置 去除editor中配置选项
3.CreateScripts中生成python打包信息 不需生成shell_config文件
4.删除无用代码和文件


-----------------2016-04-24  wanzy@putao.com--------------
version 1.1.0
1.添加ENABLE_BITCODE 设置为 NO
2.修复temp文件夹删除不干净问题。（.meta文件也要删除）。
3.重写配置文件复制到temp部分，过滤掉.meta文件（.meta文件会警告资源guid已经存在）



-----------------2016-04-20  wanzy@putao.com--------------
version 1.0.9
1.添加url schemes.在 projmods 配置文件中。葡萄星球启动游戏用。
2.ptgame_projmods移到游戏配置目录，不同的游戏url schemes 不一样。



-----------------2016-04-013  wanzy@putao.com--------------
version 1.0.8
1.重构类名，方法名称
2.编译参数中加入 isfirim , ipaoutpath，方便jenkins打包直接将ipa赋值到设置的目录。
3.修改生成的 shell 文件 内容
4.修改PutaoTool显示的选项名称。改为device. firim. appstore.


-----------------2016-04-012  wanzy@putao.com--------------
version 1.0.7
1.PTBuilder中加入CmdBuild()方法供jenkins自动打包使用，用来根据传入的参数判断类型。


-----------------2016-04-07  wanzy@putao.com--------------
version 1.0.6
1.移除project_name输入项，自动赋值为bundle_identity 的最后一部分。


-----------------2016-03-25  wanzy@putao.com--------------
version 1.0.5
1.添加jenkins编译方式。可以通过命令行传入bundle version 和 build num.
2.修改生成的shell文件。移除无用内容。
3.修改TPBuilder.重构编译方法。
4.ipa文件名中的版本号取自Unity的playersetting中。所以如果在xcode中更改版本好后。ipa文件不会对影更改。
5.添加部分注释。



-----------------2016-03-22  wanzy@putao.com--------------
version 1.0.4
1.修复当temp不存在找不到配置文件，提示temp文件不存在。添加不存在则创建一个temp。


-----------------2016-03-08  wanzy@putao.com--------------
version 1.0.3
1.增加生成ipa功能。


-----------------2016-03-08  wanzy@putao.com--------------
version 1.0.2
1.unity 中添加xcode证书设置。
2.修改ptgame.plist为KeychainAccess.plist
3.添加local预设


-----------------2016-03-07  wanzy@putao.com--------------
version 1.0.1
1.修复build_unity_all.sh中 local 和online会同时执行的问题。
2.教程中增加三个shell脚本的执行说明
3.修复online版本的ipa无法生成问题。   project_tag = "online "多了个空格。应该去掉空格。
4.生成ipa文件名称中添加 版本号。
5.生成ipa文件名称中添加 热更新资源版本号10000
6.添加自动切换architecture .online版本为universal.
7.添加自动切换ScriptingBackend。online 版本为il2cpp.其他为mono2x.
8.分离plist等配置文件.打包前遍历Application.DataPath目录，搜索config_ptgame文件夹。复制到临时文件夹PTGame/temop.
9.增加版本号输出。




----------------2016-03-04  yimf@putao.com-------------
version 1.0.0
//自动打包设置
1.项目组需要更改StartBuildAndPackage.cs中的
  code_sign_identity  ：证书
  provisioning_profile ：证书描述文件
  bundle_identity ：包名

2.点击 putaotools->BuildMakePackageShell

3.然后在mac下双击运行make_unity.sh
  注1：如果项目路径没改变 只要生成一次即可
  注2：如果无法运行 需要注册执行权限， chmod +x XXX,并将终端设置为默认启动，以后不用设置
  注3：可打开终端，运行 sh 再将 make_unity.sh拖入，敲回车，一样可运行