# Vercel 배포 가이드

이 문서는 Parking Management System을 Vercel에 배포하는 방법을 설명합니다.

## 배포 방법 1: Vercel CLI 사용 (권장)

### 1단계: Vercel CLI 로그인

```bash
vercel login
```

브라우저가 열리면 Vercel 계정으로 로그인하세요.

### 2단계: 프로젝트 배포

```bash
vercel
```

또는 프로덕션 배포를 위해:

```bash
vercel --prod
```

### 3단계: 배포 확인

배포가 완료되면 다음과 같은 정보가 표시됩니다:

```
✅  Production: https://your-project.vercel.app
🔍  Inspect: https://vercel.com/your-username/your-project
```

## 배포 방법 2: Vercel 웹 대시보드 사용

### 1단계: GitHub/GitLab에 코드 푸시

```bash
git add .
git commit -m "Initial commit for Vercel deployment"
git push origin main
```

### 2단계: Vercel 웹사이트에서 새 프로젝트 생성

1. [vercel.com](https://vercel.com)에 접속
2. GitHub/GitLab 계정으로 로그인
3. "New Project" 클릭
4. 저장소 선택
5. 프레임워크는 자동으로 `Blazor` 감지됨
6. "Deploy" 클릭

### 3단계: 배포 완료 대기

빌드 및 배포가 자동으로 진행됩니다 (보통 2-3분 소요).

## 배포 설정 확인

### vercel.json 설정

```json
{
  "buildCommand": "dotnet publish ParkingManagement/ParkingManagement.csproj -c Release -o dist",
  "outputDirectory": "dist/wwwroot",
  "framework": "blazor",
  "installCommand": "dotnet restore ParkingManagement/ParkingManagement.csproj",
  "rewrites": [
    {
      "source": "/_framework/(.*)",
      "destination": "/_framework/$1"
    },
    {
      "source": "/(.*)",
      "destination": "/index.html"
    }
  ],
  "headers": [
    {
      "source": "/(.*)",
      "headers": [
        {
          "key": "Cache-Control",
          "value": "public, max-age=0, must-revalidate"
        }
      ]
    },
    {
      "source": "/_framework/(.*)",
      "headers": [
        {
          "key": "Cache-Control",
          "value": "public, max-age=31536000, immutable"
        }
      ]
    }
  ]
}
```

## 환경 변수 설정 (필요시)

Vercel 대시보드에서 다음 환경 변수를 설정할 수 있습니다:

1. 프로젝트 설정 → Environment Variables
2. 다음 변수들을 추가:

```
ConnectionStrings__DefaultConnection = "Data Source=parking.db"
```

## 자동 배포 설정

### GitHub Actions 연동

`.github/workflows/deploy.yml` 파일을 생성하여 자동 배포를 설정할 수 있습니다:

```yaml
name: Deploy to Vercel
on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Deploy to Vercel
        uses: amondnet/vercel-action@v20
        with:
          vercel-token: ${{ secrets.VERCEL_TOKEN }}
          vercel-org-id: ${{ secrets.ORG_ID }}
          vercel-project-id: ${{ secrets.PROJECT_ID }}
          working-directory: ./
```

## 문제 해결

### 일반적인 오류

1. **빌드 실패**
   - .NET 9.0 SDK 호환성 확인
   - `dotnet restore` 명령으로 의존성 복원 확인

2. **라우팅 오류**
   - `vercel.json`의 rewrites 설정 확인
   - Blazor SPA 라우팅이 올바르게 설정되었는지 확인

3. **정적 파일 로드 실패**
   - `wwwroot` 폴더 구조 확인
   - 파일 경로가 올바른지 확인

### 로그 확인

```bash
# 배포 로그 확인
vercel logs

# 특정 배포 로그 확인
vercel logs --deployment-id <deployment-id>
```

### 로컬 테스트

배포 전에 로컬에서 빌드 테스트:

```bash
# 프로덕션 빌드
dotnet publish ParkingManagement/ParkingManagement.csproj -c Release -o dist

# 빌드 결과 확인
ls dist/wwwroot
```

## 성능 최적화

### 캐시 설정

- 정적 파일 (CSS, JS, 이미지): 1년 캐시
- HTML 파일: 캐시 없음
- Blazor 프레임워크 파일: 1년 캐시

### 압축 설정

Vercel은 자동으로 파일 압축을 수행합니다.

## 도메인 설정

### 커스텀 도메인

1. Vercel 프로젝트 설정 → Domains
2. "Add Domain" 클릭
3. 도메인 이름 입력
4. DNS 설정 안내에 따라 도메인 제공업체에서 설정

### 서브도메인

Vercel은 자동으로 `your-project.vercel.app` 형태의 서브도메인을 제공합니다.

## 모니터링

### 배포 상태 확인

```bash
vercel ls
```

### 실시간 로그

```bash
vercel logs --follow
```

## 보안 고려사항

1. **환경 변수**: 민감한 정보는 환경 변수로 설정
2. **HTTPS**: Vercel은 자동으로 HTTPS 인증서 제공
3. **CORS**: 필요한 경우 CORS 설정 추가

## 비용

- **개인 계정**: 개인 프로젝트는 무료
- **팀 계정**: 사용량에 따라 과금
- **대역폭**: 월 100GB 무료 (개인 계정)

## 지원

문제가 발생하면:
1. [Vercel 문서](https://vercel.com/docs)
2. [Blazor 문서](https://docs.microsoft.com/ko-kr/aspnet/core/blazor/)
3. GitHub Issues

---

**참고**: 이 가이드는 .NET 9.0과 Blazor WebAssembly 기준으로 작성되었습니다.
