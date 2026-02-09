# MAUI Learn - Build and Deploy Script
# This script builds and optionally deploys the application to a connected Android device

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "MAUI Learn - Build & Deploy Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Step 1: Clean previous builds
Write-Host "[1/5] Cleaning previous builds..." -ForegroundColor Yellow
dotnet clean
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Clean failed!" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Clean completed" -ForegroundColor Green
Write-Host ""

# Step 2: Restore packages
Write-Host "[2/5] Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Restore failed!" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Restore completed" -ForegroundColor Green
Write-Host ""

# Step 3: Build for Android
Write-Host "[3/5] Building for Android (Debug)..." -ForegroundColor Yellow
dotnet build -f net10.0-android -c Debug
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Build failed!" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Build completed successfully" -ForegroundColor Green
Write-Host ""

# Step 4: Check for connected devices
Write-Host "[4/5] Checking for connected Android devices..." -ForegroundColor Yellow
$adbPath = "adb"
try {
    $devices = & $adbPath devices
    $deviceCount = ($devices | Select-String "device$" | Measure-Object).Count
    
    if ($deviceCount -gt 0) {
        Write-Host "✅ Found $deviceCount connected device(s)" -ForegroundColor Green
        Write-Host ""
        Write-Host "Connected devices:" -ForegroundColor Cyan
        & $adbPath devices
    } else {
        Write-Host "⚠️  No devices connected" -ForegroundColor Yellow
        Write-Host "   To deploy, connect an Android device or start an emulator" -ForegroundColor Gray
    }
} catch {
    Write-Host "⚠️  ADB not found in PATH" -ForegroundColor Yellow
    Write-Host "   Install Android SDK Platform Tools to deploy to devices" -ForegroundColor Gray
}
Write-Host ""

# Step 5: Build output summary
Write-Host "[5/5] Build Output Summary" -ForegroundColor Yellow
Write-Host "----------------------------------------" -ForegroundColor Gray

$apkPath = "bin\Debug\net10.0-android\com.vlinder.MAUILearning-Signed.apk"
if (Test-Path $apkPath) {
    $apkSize = (Get-Item $apkPath).Length / 1MB
    Write-Host "✅ APK Generated: $apkPath" -ForegroundColor Green
    Write-Host "   Size: $([math]::Round($apkSize, 2)) MB" -ForegroundColor Gray
} else {
    Write-Host "❌ APK not found at expected location" -ForegroundColor Red
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Build Process Complete!" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Optional: Prompt for deployment
if ($deviceCount -gt 0) {
    Write-Host "Would you like to install the app on a connected device? (Y/N)" -ForegroundColor Yellow
    $response = Read-Host
    
    if ($response -eq "Y" -or $response -eq "y") {
        Write-Host ""
        Write-Host "Installing application..." -ForegroundColor Yellow
        & $adbPath install -r $apkPath
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✅ Application installed successfully!" -ForegroundColor Green
            Write-Host ""
            Write-Host "Launch the app from your device to test Firebase integration." -ForegroundColor Cyan
        } else {
            Write-Host "❌ Installation failed!" -ForegroundColor Red
        }
    }
}

Write-Host ""
Write-Host "Next Steps:" -ForegroundColor Cyan
Write-Host "1. Deploy the APK to your Android device or emulator" -ForegroundColor Gray
Write-Host "2. Launch the app and verify it starts without crashes" -ForegroundColor Gray
Write-Host "3. Check debug logs for Firebase initialization messages" -ForegroundColor Gray
Write-Host "4. Test push notifications from Firebase Console" -ForegroundColor Gray
Write-Host ""
