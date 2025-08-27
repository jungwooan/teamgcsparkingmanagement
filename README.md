# Parking Management System

Blazor WebAssembly를 사용한 주차 관리 시스템입니다.

## 기술 스택

- **Frontend**: Blazor WebAssembly (.NET 9.0)
- **Database**: Entity Framework Core (SQLite)
- **UI Framework**: Bootstrap 5
- **Deployment**: Vercel

## 로컬 개발 환경 설정

### 필수 요구사항

- .NET 9.0 SDK
- Visual Studio 2022 또는 VS Code

### 설치 및 실행

1. 저장소 클론
```bash
git clone <repository-url>
cd Parking
```

2. 의존성 복원
```bash
dotnet restore ParkingManagement/ParkingManagement.csproj
```

3. 프로젝트 빌드
```bash
dotnet build ParkingManagement/ParkingManagement.csproj
```

4. 개발 서버 실행
```bash
dotnet run --project ParkingManagement/ParkingManagement.csproj
```

5. 브라우저에서 `https://localhost:7000` 접속

## Vercel 배포

이 프로젝트는 Vercel에서 자동으로 배포되도록 설정되어 있습니다.

### 배포 단계

1. **GitHub/GitLab에 코드 푸시**
```bash
git add .
git commit -m "Initial commit"
git push origin main
```

2. **Vercel 대시보드에서 프로젝트 연결**
   - [Vercel](https://vercel.com)에 로그인
   - "New Project" 클릭
   - GitHub/GitLab 저장소 선택
   - 프레임워크는 `Blazor` 자동 감지됨

3. **자동 배포 확인**
   - 빌드 및 배포가 자동으로 진행됨
   - 배포 완료 후 제공되는 URL로 접속

### 배포 설정

`vercel.json` 파일에 다음 설정이 포함되어 있습니다:

- **빌드 명령**: `dotnet publish ParkingManagement/ParkingManagement.csproj -c Release -o dist`
- **출력 디렉토리**: `dist/wwwroot`
- **프레임워크**: `blazor`
- **SPA 라우팅**: 모든 경로를 `index.html`로 리다이렉트
- **캐시 설정**: 정적 파일 최적화

## 프로젝트 구조

```
Parking/
├── ParkingManagement/          # Blazor WebAssembly 프로젝트
│   ├── Data/                   # 데이터베이스 컨텍스트
│   ├── Models/                 # 데이터 모델
│   ├── Pages/                  # Blazor 페이지
│   ├── Services/               # 서비스 클래스
│   ├── Shared/                 # 공유 컴포넌트
│   └── wwwroot/                # 정적 파일
├── vercel.json                 # Vercel 배포 설정
└── README.md                   # 프로젝트 문서
```

## 주요 기능

- 사용자 인증 및 권한 관리
- 주차장 등록 및 관리
- 주차 기록 추적
- 통계 및 리포트
- 반응형 웹 디자인

## 환경 변수

프로덕션 환경에서 다음 환경 변수 설정이 필요할 수 있습니다:

- `ConnectionStrings__DefaultConnection`: 데이터베이스 연결 문자열
- 기타 보안 관련 설정

## 문제 해결

### 빌드 오류
- .NET 9.0 SDK가 설치되어 있는지 확인
- `dotnet restore` 명령으로 의존성 복원 확인

### 배포 오류
- Vercel 대시보드에서 빌드 로그 확인
- `vercel.json` 설정 검증
- 프로젝트 구조가 올바른지 확인

## 라이선스

이 프로젝트는 MIT 라이선스 하에 배포됩니다.
