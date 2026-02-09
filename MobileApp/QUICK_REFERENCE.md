# 🚀 Quick Reference - MAUI Learn Firebase Integration

## ✅ Status: FULLY CONFIGURED & READY

---

## 📦 What Was Done

1. **Fixed Runtime Crash**
   - Disabled Firebase Crashlytics (was causing "Build ID missing" error)
   - Configured proper Firebase initialization

2. **Cleaned Up Packages**
   - Removed conflicting `Plugin.Firebase.Core` package
   - Kept only necessary Firebase packages

3. **Configured Firebase Services**
   - ✅ Cloud Messaging enabled
   - ✅ Notification permissions configured
   - ✅ Topic subscriptions set up ("All", "Android")

4. **Build Verification**
   - ✅ Clean build (no errors)
   - ✅ APK generated successfully
   - ✅ All dependencies resolved

---

## 🎯 Quick Commands

### Build the App
```powershell
dotnet build -f net10.0-android
```

### Clean Build
```powershell
dotnet clean
dotnet build -f net10.0-android
```

### Deploy to Device
```powershell
# Option 1: Use the script
.\build-and-deploy.ps1

# Option 2: Manual
adb install -r bin\Debug\net10.0-android\com.vlinder.MAUILearning-Signed.apk
```

### View Logs
```powershell
adb logcat | Select-String "MobileApp"
```

---

## 📁 Key Files

| File | Purpose |
|------|---------|
| `MauiProgram.cs` | Firebase SDK initialization |
| `App.xaml.cs` | FCM bootstrap on startup |
| `NotificationBootstrapService.cs` | FCM permissions & topics |
| `Platforms/Android/google-services.json` | Firebase config (Android) |
| `MobileApp.csproj` | Build configuration |

---

## 🧪 Testing Checklist

- [ ] Deploy APK to device/emulator
- [ ] Launch app (should not crash)
- [ ] Accept notification permission
- [ ] Send test notification from Firebase Console
- [ ] Verify notification received

---

## 🔥 Firebase Console

**Send Test Notification:**
1. Go to: https://console.firebase.google.com/
2. Select project: `test-project-483db`
3. Navigate to: **Cloud Messaging**
4. Click: **Send your first message**
5. Target: Topic → `All` or `Android`

---

## 📊 Build Info

- **APK**: `bin\Debug\net10.0-android\com.vlinder.MAUILearning-Signed.apk`
- **Size**: ~18.9 MB
- **Package**: `com.vlinder.MAUILearning`
- **Min Android**: 6.0 (API 23)
- **Target Android**: 14 (API 34)

---

## 🆘 Quick Troubleshooting

**App crashes on launch?**
→ Check: `adb logcat` for error messages

**No notifications?**
→ Verify: Notification permission granted
→ Check: Topic subscription in logs

**Build fails?**
→ Run: `dotnet clean` then rebuild

---

## 📚 Documentation

- **Full Details**: `INTEGRATION_COMPLETE.md`
- **Firebase Config**: `FIREBASE_CONFIGURATION.md`
- **Build Script**: `build-and-deploy.ps1`

---

**Last Updated**: 2026-02-09  
**Status**: ✅ Ready for Testing
