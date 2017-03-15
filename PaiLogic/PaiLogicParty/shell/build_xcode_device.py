import os
import sys
import time
version_bundle=sys.argv[1]
version_buildnum=sys.argv[2]
len_argv=len(sys.argv)
if len_argv>=4:
    isjenkins=sys.argv[3]
    ipaoutpath=sys.argv[4]
else :
    isjenkins=False
    ipaoutpath=''
path_project=sys.path[0]
path_project = os.path.dirname(path_project)
os.system("cd %s;xcodebuild -project %s -target %s CODE_SIGN_IDENTITY='%s' PROVISIONING_PROFILE='%s' PRODUCT_BUNDLE_IDENTIFIER='%s' PRODUCT_NAME='%s' -configuration %s"%(path_project+"/Assets",path_project+"/XCode_Unity/Unity-iPhone.xcodeproj","Unity-iPhone","iPhone Developer: Jiaqing Zou (SC4FCXNH94)","52e5f8aa-793f-4700-9437-532f2a95bb04","com.putao.modou.cnhouse","cn","Release"))
if ipaoutpath.strip()=='':
    os.system("cd %s;xcrun -sdk iphoneos PackageApplication -v %s -o %s CODE_SIGN_IDENTITY='%s'"%(path_project+"/Assets",path_project+"/XCode_Unity/build/Release-iphoneos/cn.app",path_project+"/"+"cn"+"_"+version_bundle+"_"+version_buildnum+"_"+time.strftime("%Y%m%d%H%M",time.localtime())+".ipa","iPhone Developer: Jiaqing Zou (SC4FCXNH94)"))
else :
    os.system("cd %s;xcrun -sdk iphoneos PackageApplication -v %s -o %s CODE_SIGN_IDENTITY='%s'"%(path_project+"/Assets",path_project+"/XCode_Unity/build/Release-iphoneos/cn.app",ipaoutpath+"/"+"cn"+"_"+version_bundle+"_"+version_buildnum+"_"+time.strftime("%Y%m%d%H%M",time.localtime())+".ipa","iPhone Developer: Jiaqing Zou (SC4FCXNH94)"))

os.system("cd %s;rm -rf %s"%(path_project+"/Assets",path_project+"/XCode_Unity/build/Release-iphoneos/cn.app",))