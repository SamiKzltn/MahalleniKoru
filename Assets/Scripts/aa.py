import zipfile

# Şifreli dosyanın yolu
zip_file_path = r"C:\\Users\\DELL\\Desktop\\Ss.zip"  # ZIP dosyasının adı ve yolu
# Kombinasyon listesinin yolu
combinations_file_path = r"C:\\Users\\DELL\\Desktop\\all_com.txt"

# Kombinasyonları oku
with open(combinations_file_path, "r") as file:
    combinations = file.readlines()

# ZIP dosyasını açmak için brute force
with zipfile.ZipFile(zip_file_path) as zfile:
    for combination in combinations:
        password = combination.strip().encode('utf-8')  # Şifreyi al
        try:
            print(f"Deniyor: {password.decode('utf-8')}")
            zfile.extractall(pwd=password)  # Dosyayı çıkart
            print(f"Şifre bulundu: {password.decode('utf-8')}")
            break  # Şifre bulundu, durdur
        except (RuntimeError, zipfile.BadZipFile) as e:
            print(f"Yanlış şifre: {password.decode('utf-8')} - Hata: {e}")
            continue  # Yanlış şifreyi geç
