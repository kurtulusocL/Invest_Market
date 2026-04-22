using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess.EntityFramework;
using Investigation.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Repositories
{
    public class AdRepository : EntityRepositoryBase<Ad, ApplicationDbContext>, IAdRepository
    {
        readonly ApplicationDbContext _context;
        public AdRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Ad> GetAllPersonalizedAdsForUser(string userId)
        {
            const int count = 5;
            try
            {
                var now = DateTime.Now;
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                var userAge = 0;
                var userCountry = "";

                var blogLikes = 0;
                var blogSaves = 0;
                var investorLikes = 0;
                var investorSaves = 0;
                var companyLikes = 0;
                var companySaves = 0;
                var postLikes = 0;

                if (user != null)
                {
                    userAge = user.Birthdate != DateTime.MinValue
                        ? AgeCalculatorHelper.CalculateAge(user.Birthdate)
                        : 0;

                    userCountry = user.Country?.Trim()?.ToUpperInvariant() ?? "";

                    blogLikes = _context.Likes.Count(l => l.AppUserId == userId && l.BlogId != null);
                    blogSaves = _context.SavedContents.Count(s => s.AppUserId == userId && s.BlogId != null);

                    investorLikes = _context.Likes.Count(l => l.AppUserId == userId && l.InvestorId != null);
                    investorSaves = _context.SavedContents.Count(s => s.AppUserId == userId && s.InvestorId != null);

                    companyLikes = _context.Likes.Count(l => l.AppUserId == userId && l.CompanyId != null);
                    companySaves = _context.SavedContents.Count(s => s.AppUserId == userId && s.CompanyId != null);

                    postLikes = _context.Likes.Count(l => l.AppUserId == userId && l.PostId != null);
                }

                var activeAds = _context.Ads
                    .Include(a => a.AdTargets)
                    .Where(a => a.StartDate <= now && a.FinishDate >= now && !a.IsDeleted && a.IsActive)
                    .ToList();

                var matchedAds = new List<Ad>();
                var usedAdIds = new HashSet<int>();

                var eligiblePersonalizedAds = new List<(Ad ad, int priority)>();

                foreach (var ad in activeAds.Where(a => a.AdTargets.Any()))
                {
                    if (usedAdIds.Contains(ad.Id)) continue;

                    foreach (var target in ad.AdTargets)
                    {
                        if (target.MinAge.HasValue && userAge > 0 && userAge < target.MinAge.Value) continue;
                        if (target.MaxAge.HasValue && userAge > 0 && userAge > target.MaxAge.Value) continue;

                        if (!string.IsNullOrEmpty(target.TargetCountries))
                        {
                            var targetCountries = target.TargetCountries
                                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(c => c.Trim().ToUpperInvariant())
                                .ToHashSet();

                            if (!string.IsNullOrEmpty(userCountry) && !targetCountries.Contains(userCountry)) continue;
                        }

                        if (target.IncludeBlogInteractions)
                        {
                            if (target.MinTotalLikeCount.HasValue && blogLikes < target.MinTotalLikeCount.Value) continue;
                            if (target.MinTotalSaveCount.HasValue && blogSaves < target.MinTotalSaveCount.Value) continue;
                        }

                        if (target.IncludeInvestorInteractions)
                        {
                            if (target.MinTotalLikeCount.HasValue && investorLikes < target.MinTotalLikeCount.Value) continue;
                            if (target.MinTotalSaveCount.HasValue && investorSaves < target.MinTotalSaveCount.Value) continue;
                        }

                        if (target.IncludeCompanyInteractions)
                        {
                            if (target.MinTotalLikeCount.HasValue && companyLikes < target.MinTotalLikeCount.Value) continue;
                            if (target.MinTotalSaveCount.HasValue && companySaves < target.MinTotalSaveCount.Value) continue;
                        }

                        if (target.IncludePostInteractions)
                        {
                            if (target.MinTotalLikeCount.HasValue && postLikes < target.MinTotalLikeCount.Value) continue;
                        }

                        eligiblePersonalizedAds.Add((ad, target.MinInteractionCount));
                        break;
                    }
                }

                eligiblePersonalizedAds.Sort((x, y) => x.priority.CompareTo(y.priority));

                foreach (var (ad, _) in eligiblePersonalizedAds)
                {
                    if (usedAdIds.Contains(ad.Id)) continue;
                    matchedAds.Add(ad);
                    usedAdIds.Add(ad.Id);
                    if (matchedAds.Count >= count) break;
                }

                // Eğer kullanıcı hedeflenmiş reklam kriterlerine uymuyorsa, hedeflenmemiş reklamlar göster
                if (matchedAds.Count == 0)
                {
                    var randomAds = activeAds
                        .Where(a => !usedAdIds.Contains(a.Id))
                        .OrderBy(_ => Guid.NewGuid()) // Daha güvenilir random sıralama
                        .Take(count)
                        .ToList();

                    matchedAds.AddRange(randomAds);
                }
                else if (matchedAds.Count < count)
                {
                    var remainingCount = count - matchedAds.Count;
                    var fillerAds = activeAds
                        .Where(a => !usedAdIds.Contains(a.Id))
                        .DistinctBy(a => a.Id)
                        .OrderBy(_ => Guid.NewGuid())
                        .Take(remainingCount)
                        .ToList();

                    matchedAds.AddRange(fillerAds);
                }
                return matchedAds;




                //var now = DateTime.Now;
                //var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                //var userAge = 0;
                //var userCountry = "";

                //// Kullanıcının etkileşim sayılarını al
                //var blogLikes = 0;
                //var blogSaves = 0;
                //var investorLikes = 0;
                //var investorSaves = 0;
                //var companyLikes = 0;
                //var companySaves = 0;
                //var postLikes = 0;

                //if (user != null)
                //{
                //    userAge = user.Birthdate != DateTime.MinValue
                //        ? AgeCalculatorHelper.CalculateAge(user.Birthdate)
                //        : 0;

                //    userCountry = user.Country?.Trim()?.ToUpperInvariant() ?? "";

                //    // Etkileşim sayılarını hesapla
                //    blogLikes = _context.Likes.Count(l => l.AppUserId == userId && l.BlogId != null);
                //    blogSaves = _context.SavedContents.Count(s => s.AppUserId == userId && s.BlogId != null);

                //    investorLikes = _context.Likes.Count(l => l.AppUserId == userId && l.InvestorId != null);
                //    investorSaves = _context.SavedContents.Count(s => s.AppUserId == userId && s.InvestorId != null);

                //    companyLikes = _context.Likes.Count(l => l.AppUserId == userId && l.CompanyId != null);
                //    companySaves = _context.SavedContents.Count(s => s.AppUserId == userId && s.CompanyId != null);

                //    postLikes = _context.Likes.Count(l => l.AppUserId == userId && l.PostId != null);
                //}

                //var activeAds = _context.Ads
                //    .Include(a => a.AdTargets)
                //    .Where(a => a.StartDate <= now && a.FinishDate >= now && !a.IsDeleted && a.IsActive)
                //    .ToList();

                //var personalizedAds = activeAds.Where(a => a.AdTargets.Any()).ToList();
                //var generalAds = activeAds.Where(a => !a.AdTargets.Any()).ToList();

                //var matchedAds = new List<Ad>();
                //var usedAdIds = new HashSet<int>();

                //var eligiblePersonalizedAds = new List<(Ad ad, int priority)>(); // priority = MinInteractionCount

                //foreach (var ad in personalizedAds)
                //{
                //    if (usedAdIds.Contains(ad.Id)) continue;

                //    var isEligible = false;

                //    foreach (var target in ad.AdTargets)
                //    {
                //        // YAŞ KONTROLÜ
                //        if (target.MinAge.HasValue && userAge > 0 && userAge < target.MinAge.Value) continue;
                //        if (target.MaxAge.HasValue && userAge > 0 && userAge > target.MaxAge.Value) continue;

                //        // ÜLKE KONTROLÜ
                //        if (!string.IsNullOrEmpty(target.TargetCountries))
                //        {
                //            var targetCountries = target.TargetCountries
                //                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                //                .Select(c => c.Trim().ToUpperInvariant())
                //                .ToHashSet();

                //            if (!string.IsNullOrEmpty(userCountry) && !targetCountries.Contains(userCountry)) continue;
                //        }

                //        // --- ETKİLEŞİM KONTROLLERİ ---
                //        // Blog etkileşimleri
                //        if (target.IncludeBlogInteractions)
                //        {
                //            if (target.MinTotalLikeCount.HasValue && blogLikes < target.MinTotalLikeCount.Value) continue;
                //            if (target.MinTotalSaveCount.HasValue && blogSaves < target.MinTotalSaveCount.Value) continue;
                //        }

                //        // Investor etkileşimleri
                //        if (target.IncludeInvestorInteractions)
                //        {
                //            if (target.MinTotalLikeCount.HasValue && investorLikes < target.MinTotalLikeCount.Value) continue;
                //            if (target.MinTotalSaveCount.HasValue && investorSaves < target.MinTotalSaveCount.Value) continue;
                //        }

                //        // Company etkileşimleri
                //        if (target.IncludeCompanyInteractions)
                //        {
                //            if (target.MinTotalLikeCount.HasValue && companyLikes < target.MinTotalLikeCount.Value) continue;
                //            if (target.MinTotalSaveCount.HasValue && companySaves < target.MinTotalSaveCount.Value) continue;
                //        }

                //        // Post etkileşimleri
                //        if (target.IncludePostInteractions)
                //        {
                //            if (target.MinTotalLikeCount.HasValue && postLikes < target.MinTotalLikeCount.Value) continue;
                //            // Save count yok, sadece like count var
                //        }

                //        // Uygunsa, MinInteractionCount ile öncelik al
                //        isEligible = true;
                //        eligiblePersonalizedAds.Add((ad, target.MinInteractionCount));
                //        break; // Bir hedef uygunsa yeterli
                //    }
                //}

                //// Uygun hedeflenmiş reklamları öncelik sırasına göre sırala (küçük = yüksek öncelik)
                //eligiblePersonalizedAds.Sort((x, y) => x.priority.CompareTo(y.priority));

                //// En yüksek öncelikli uygun reklamları ekle
                //foreach (var (ad, _) in eligiblePersonalizedAds)
                //{
                //    if (usedAdIds.Contains(ad.Id)) continue;
                //    matchedAds.Add(ad);
                //    usedAdIds.Add(ad.Id);
                //    if (matchedAds.Count >= count) break;
                //}

                //// Hedeflenmiş reklamlar yetmiyorsa, hedeflenmemiş reklamlarla tamamla
                //if (matchedAds.Count < count)
                //{
                //    var remainingCount = count - matchedAds.Count;
                //    var generalAdsPool = generalAds
                //        .Where(a => !usedAdIds.Contains(a.Id))
                //        .OrderBy(_ => Random.Shared.Next())
                //        .Take(remainingCount)
                //        .ToList();

                //    matchedAds.AddRange(generalAdsPool);
                //    foreach (var ad in generalAdsPool) usedAdIds.Add(ad.Id);
                //}

                //// Hâlâ eksikse, tüm aktif reklamlar arasından rastgele tamamla
                //if (matchedAds.Count < count)
                //{
                //    var remainingCount = count - matchedAds.Count;
                //    var allAvailableAds = activeAds
                //        .Where(a => !usedAdIds.Contains(a.Id))
                //        .OrderBy(_ => Random.Shared.Next())
                //        .Take(remainingCount)
                //        .ToList();

                //    matchedAds.AddRange(allAvailableAds);
                //}

                //return matchedAds;
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public Ad ReadNonUniqueHit(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                var hitRead = _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefault();
                if (hitRead != null && hitRead.NonUniqueHit >= 0)
                {
                    hitRead.NonUniqueHit++;
                    _context.SaveChanges();
                    return hitRead;
                }
                hitRead.NonUniqueHit = 0;
                _context.SaveChanges();
                return hitRead;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Hit the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = true;

                var hits = await _context.Set<Hit>().Where(a => a.AdId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var adTargets = await _context.Set<AdTarget>().Where(a => a.AdId == id).ToListAsync();
                foreach (var adTarget in adTargets)
                {
                    adTarget.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Active the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.UtcNow;

                var hits = await _context.Set<Hit>().Where(a => a.AdId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.UtcNow;
                }

                var adTargets = await _context.Set<AdTarget>().Where(a => a.AdId == id).ToListAsync();
                foreach (var adTarget in adTargets)
                {
                    adTarget.IsActive = false;
                    adTarget.SuspendedDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting DeActive the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            try
            {
                var data = await _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.UtcNow;

                var hits = await _context.Set<Hit>().Where(a => a.AdId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.UtcNow;
                }

                var adTargets = await _context.Set<AdTarget>().Where(a => a.AdId == id).ToListAsync();
                foreach (var adTarget in adTargets)
                {
                    adTarget.IsDeleted = true;
                    adTarget.DeletedDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var data = await _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var hits = await _context.Set<Hit>().Where(a => a.AdId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var adTargets = await _context.Set<AdTarget>().Where(a => a.AdId == id).ToListAsync();
                foreach (var adTarget in adTargets)
                {
                    adTarget.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }
    }
}
