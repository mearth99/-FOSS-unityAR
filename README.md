# FOSS unityAR

유니티 AR Foundation과 AR core를 사용하여 안드로이드 디바이스용 어플을 제작한다.

유니티 파일은 용량이 매우 큰 관계로, Assets 폴더만 업로드했다.

1. 유니티를 설치하기 위해선 10GB 이상의 용량이 필요하며, 유니티를 회원가입하여 사용할 수 있다.
개인의 경우 무료이며, Unity hub를 설치받아 hub에서 유니티를 설치할 수 있다.
유니티 hub에서 최신버전으로 나타났던 2021.3.5f1 버전을 사용하였다. 다만, 2019에서 나타났던 Grade 버전 오류는 
2020부터는 해결되었기 때문에 2020 이상 버전을 사용한다면, 최신버전 관계없이 사용할 수 있을 것이다.

2. 유니티를 설치한 이후에는 부속(모듈)을 추가로 설치할 필요가 있다. 안드로이드에서 사용하기 위해서는
플랫폼 Android Bulid Support에 해당하는 걸 전부 설치해야한다. 하위 설치로 Android SDK & NDK Tools와 OpenJDK가 있는데 이 둘 다 추가로 설치하면 되겠다. 유니티는 C#기반 언어로, VS code를 이용하던지, Microsoft Visual Studio를 사용하여 연동하는 걸 추천한다. 

3. 설치가 마무리 된다면, 유니티를 실행하여 강의를 참고하면 된다. 강의 중 환경설정을 설명하자면 
Window - Package 에서 Packages: In Project 로 되어 있는 부분을 Unity Registry로 변경하고 AR을 검색하여 AR Foundation, AR Core(플러그인)를 설치하면 된다. 설치가 마무리 된다면, File - Bulid Setting에 들어가 안드로이드 모양을 클릭하여 Switch Platform을 눌러줘 변경해주면 된다.

4. Player Settings을 눌러 Player창에서 Company Name와 Product Name을 변경해줄 수 있고, Player 창에 안드로이드 모양을 눌러서
Other Settings를 설정해야한다. 이 중 Auto Graphics API 체크를 해제하고, Multithreaded Rendering 체크 해제하고
Minimun API Level을 최소 7.0 이상으로 설정해주면 된다. 마지막으로 XR Plug -in (왼쪽 탭)을 눌러서 AR Core를 체크해주면 된다.

5. 지금 Github - Script에 C#코드가 작성되어 있는데, 유니티로 프로젝트를 생성했다면, 유니티 프로젝트 폴더가 존재한다.
이 Assets - Script폴더만 받아서 붙여넣기하면된다. 이후 component 추가로 사용하면 되겠다.

# APK는 Distance 어플이다.
