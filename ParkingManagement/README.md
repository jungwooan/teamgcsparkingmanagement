# 주차 관리 시스템 (Parking Management System)

Blazor WebAssembly를 사용하여 개발된 팀 주차권 관리 웹 애플리케이션입니다.

## 주요 기능

### 📅 캘린더 뷰
- 월별 캘린더로 주차 현황을 한눈에 확인
- 각 날짜별 주차권 사용, 반납, 외부 주차 현황 표시
- 날짜 클릭 시 상세 정보 모달 표시

### 👥 사용자 관리
- 사용자 등록, 수정, 삭제
- 사용자별 주차 통계 확인
- 카카오톡 연동 지원 (향후 구현 예정)

### 📊 통계 및 분석
- 사용자별 주차 사용 횟수
- 외부 주차 비용 집계
- 월별 통계 리포트

### 📱 모바일 최적화
- 반응형 디자인으로 모바일에서도 편리한 사용
- 터치 친화적 인터페이스

## 기술 스택

- **Frontend**: Blazor WebAssembly
- **Language**: C#
- **Storage**: Local Storage (브라우저)
- **UI Framework**: Bootstrap 5
- **Icons**: Open Iconic

## 설치 및 실행

### 필수 요구사항
- .NET 9.0 SDK 이상
- 웹 브라우저 (Chrome, Firefox, Safari, Edge)

### 실행 방법

1. 프로젝트 클론 또는 다운로드
```bash
git clone [repository-url]
cd ParkingManagement
```

2. 의존성 복원
```bash
dotnet restore
```

3. 프로젝트 빌드
```bash
dotnet build
```

4. 개발 서버 실행
```bash
dotnet run
```

5. 브라우저에서 접속
```
https://localhost:5001
```

## 사용법

### 1. 사용자 등록
1. "사용자 관리" 메뉴로 이동
2. "새 사용자 추가" 버튼 클릭
3. 이름, 이메일, 카카오 ID (선택사항) 입력
4. 저장

### 2. 주차 기록 관리
1. 메인 캘린더에서 날짜 클릭
2. 해당 날짜의 주차 현황 확인
3. "사용 기록 추가", "반납 기록 추가", "외부 주차 추가" 버튼으로 기록 입력

### 3. 통계 확인
1. "사용자 관리"에서 사용자별 통계 확인
2. 각 사용자의 주차 사용 횟수, 외부 주차 비용 등 확인

## 데이터 저장

현재 버전에서는 브라우저의 Local Storage를 사용하여 데이터를 저장합니다.
- 브라우저를 닫아도 데이터가 유지됩니다
- 다른 브라우저나 기기에서는 데이터가 공유되지 않습니다
- 브라우저 데이터 삭제 시 모든 데이터가 손실됩니다

## 향후 개발 계획

### 카카오톡 연동
- 카카오 로그인 구현
- 카카오톡 알림 기능 추가
- 카카오톡 채널 연동

### 서버 배포
- 무료 클라우드 서버 배포 (Azure, Heroku 등)
- 데이터베이스 연동 (SQLite, PostgreSQL)
- 다중 사용자 데이터 동기화

### 추가 기능
- 주차권 예약 시스템
- 주차장 위치 정보
- 비용 정산 기능
- 리포트 생성 및 내보내기

## 프로젝트 구조

```
ParkingManagement/
├── Models/                 # 데이터 모델
│   ├── User.cs
│   ├── ParkingRecord.cs
│   ├── ParkingUsage.cs
│   ├── ParkingReturn.cs
│   └── ExternalParking.cs
├── Services/              # 비즈니스 로직
│   ├── ILocalStorageService.cs
│   ├── LocalStorageService.cs
│   ├── IDataService.cs
│   └── DataService.cs
├── Pages/                 # Blazor 페이지
│   ├── Calendar.razor
│   └── Users.razor
├── Shared/               # 공유 컴포넌트
│   └── NavMenu.razor
└── wwwroot/              # 정적 파일
    └── css/
        └── app.css
```

## 라이선스

이 프로젝트는 MIT 라이선스 하에 배포됩니다.

## 기여하기

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 문의사항

프로젝트에 대한 문의사항이나 버그 리포트는 Issues 탭을 이용해 주세요.
