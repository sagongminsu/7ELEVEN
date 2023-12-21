# 7ELEVEN

                                                    구현사항
{
---
조작키 
이동: W,A,S,D
점프: Space
인벤토리: TAB
건축: B
제작: Q
---

필수 구현 사항

1.자원 수집 및 가공

플레이어가 자원을 찾아서 수집하고, 이를 가공하여 생존에 필요한 아이템을 제작할 수 있도록 합니다.(작업대 제작)
자원 수집과 가공 메커니즘을 구현합니다.(나무 캐기, 돌 캐기)

2.식사관리

플레이어의 캐릭터가 식사을 관리해야 하며, 굶주림을 방지해야 합니다.(요리 하기)
식사와 수분 관리 시스템을 구현하여 생존 요소를 추가합니다.(UI)

3.건축 및 생존 기지 구축

플레이어가 기지를 건축하고 안전한 곳을 만들 수 있도록 합니다.
건축 및 기지 관리 메커니즘을 구현합니다.

4.적과의 전투

다양한 적과의 전투를 구현하고, 적의 AI를 제공하여 플레이어에게 도전을 제공합니다.
전투 시스템과 AI를 개발합니다.

5.생존 관리 시스템

플레이어의 체력,스태미너 생존과 관련된 상태를 관리합니다.
생존 관리 시스템을 설계하고 구현합니다.

6.자원 리스폰

자원이 다시 생성되는 시스템을 구현하여 게임의 지속 가능성을 유지합니다.
자원 리스폰 주기 및 메커니즘을 설계합니다.

--------------------------------------------------
추가 구현사항
1.고급 건축 시스템
다양한 건축 옵션과 고급 건축 요소를 추가하여 기지 건설을 더 다양하게 만듭니다.
건축 및 구조물 개발 시스템을 확장합니다.
2.다양한 적 종류
다양한 종류의 적을 추가하여 게임의 다양성을 높입니다.
다양한 동물과 적 캐릭터를 구현합니다.
3.크래프팅 시스템
다양한 아이템과 장비를 제작하기 위한 복잡한 크래프팅 시스템을 도입합니다.
아이템 제작 및 조합 메커니즘을 확장합니다.
4.사운드 및 음악
게임에 사운드 효과와 음악을 추가하여 게임의 분위기를 개선합니다.
자연 소리, 적의 소리 효과 등을 통해 게임 환경을 풍부하게 만듭니다.(사운드 온오프 기능구현)
}











{
트러블슈팅

오브젝트 Layer 설정값 변동
merge 진행하면서 오브젝트들의 Layer 가 바뀌는 경우 발생.
해결 : 테스트를 진행할 때마다 재확인 및 재설정

건축물을 지면이나 다른 건축물에 붙여서 설치
해결 : 카메라 기준으로 어느 정도 거리에서 수직레이를 생성하여 레이 충돌을 체크하여 충돌된 거리만큼 오브젝트의 Y값을 감소 시킴

깃허브를 이용하면서 오류 발생
합치면서 오류가 발생하여 파일이 날라가는 문제가 발생
해결: Main을 복사하여서 백업파일 만들어서 오류가 발생하여도 복구가 되도록 했다.

AI 움직임 에러
적이나 동물이 죽었는데 시체가 플레이어를 따라오는 현상.
해결 : GetComponent 를 통해 Nav Mesh Agent 의 속도를 0 으로 설정

자원 스폰 시 좌표 정하기
Terrain 높낮이에 따라 자원스폰 위치를 바꿔 줘야하는 문제.
해결 : Physics.RayCast 를 하늘에서 아래로 쏘고 Terrain과 부딪히는 위치에 자원 생성

CSV 파일 가져오기
CSV 파일을 어떻게 가져와서 어떤식으로 아이템을 장착해야 할지.
해결 : CSV의 데이터를 로드 해주는 스크립트에서 Prefab 주소를 저장해 줄 변수를 만들어 할당

---
프로젝트 소감

사공민수
팀 프로젝트를 하면서 문제가 발생하면 팀원들과 같이 해결하면서 많은 것을 배울 수 있어서 좋았습니다.

노우진
모두 적극적으로 참여하여 야근까지 하는 모습을 보여주며 열심히 작업하였습니다. 다들 고생 많으셨습니다.

선건우
팀원들 모두 야간까지 일하느라 고생하셨습니다.

이철희
항상 긍정적으로 소통하는팀원 분들 덕분에 마음 편히 작업 할 수 있었습니다. 고생하셨습니다.

임석창
부족한 실력임에도 불구하고팀원들이 많은 도움을 주셔서너무 감사했습니다. 앞으로도 화이팅 하시길 바랍니다!

조형승
배움의 즐거움을 느낄 수 있는 귀한 시간이었습니다. 조원들 덕분에 아주 든든했습니다. 감사합니다 :)
}










