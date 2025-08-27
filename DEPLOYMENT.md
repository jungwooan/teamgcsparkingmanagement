# Vercel 배포 가이드

## 사전 요구사항

1. [Vercel 계정](https://vercel.com) 생성
2. [Git](https://git-scm.com/) 설치
3. [.NET 9.0 SDK](https://dotnet.microsoft.com/download) 설치

## 배포 방법

### 방법 1: Vercel CLI 사용

1. Vercel CLI 설치:
```bash
npm i -g vercel
```

2. 프로젝트 루트에서 로그인:
```bash
vercel login
```

3. 배포:
```bash
vercel --prod
```

### 방법 2: Vercel 웹 인터페이스 사용

1. [vercel.com](https://vercel.com)에 로그인
2. "New Project" 클릭
3. GitHub/GitLab/Bitbucket에서 저장소 연결
4. 프로젝트 설정:
   - **Framework Preset**: Blazor
   - **Build Command**: `cd ParkingManagement && dotnet publish -c Release -o ./publish`
   - **Output Directory**: `ParkingManagement/publish`
   - **Install Command**: `cd ParkingManagement && dotnet restore`

## 환경 변수 설정

필요한 경우 Vercel 대시보드에서 환경 변수를 설정할 수 있습니다:

1. 프로젝트 설정 → Environment Variables
2. 필요한 환경 변수 추가

## 주의사항

- Blazor WebAssembly는 클라이언트 사이드에서 실행되므로 서버 API가 필요한 경우 별도로 배포해야 합니다.
- 현재 데이터는 브라우저 로컬 스토리지를 사용하므로 서버가 필요하지 않습니다.

## 문제 해결

### 빌드 오류
- .NET 9.0 SDK가 설치되어 있는지 확인
- 프로젝트 파일의 PackageReference가 올바른지 확인

### 라우팅 오류
- `vercel.json`의 routes 설정이 올바른지 확인
- `public/_redirects` 파일이 있는지 확인

### WASM 로딩 오류
- `vercel.json`의 headers 설정에서 WASM 파일의 Content-Type이 올바르게 설정되어 있는지 확인
