﻿using QMTech.Core;
using QMTech.Core.Data;
using QMTech.Core.Domain.Catalog;
using QMTech.Core.Domain.Common;
using QMTech.Core.Domain.Customers;
using QMTech.Core.Domain.Messages;
using QMTech.Core.Domain.Security;
using QMTech.Core.Infrastructure;
using QMTech.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMTech.Services.Installation
{
    public partial class CodeFirstInstallationService : IInstallationService
    {
        #region Fields

        private readonly IRepository<Store> _storeRepository;
        //private readonly IRepository<MeasureDimension> _measureDimensionRepository;
        //private readonly IRepository<MeasureWeight> _measureWeightRepository;
        //private readonly IRepository<TaxCategory> _taxCategoryRepository;
        //private readonly IRepository<Language> _languageRepository;
        //private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        //private readonly IRepository<SpecificationAttribute> _specificationAttributeRepository;
        //private readonly IRepository<CheckoutAttribute> _checkoutAttributeRepository;
        //private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<StoreCategory> _categoryRepository;
        //private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IRepository<Product> _productRepository;
        //private readonly IRepository<UrlRecord> _urlRecordRepository;
        //private readonly IRepository<RelatedProduct> _relatedProductRepository;
        private readonly IRepository<EmailAccount> _emailAccountRepository;
        private readonly IRepository<MessageTemplate> _messageTemplateRepository;
        //private readonly IRepository<ForumGroup> _forumGroupRepository;
        //private readonly IRepository<Forum> _forumRepository;
        //private readonly IRepository<Country> _countryRepository;
        //private readonly IRepository<StateProvince> _stateProvinceRepository;
        //private readonly IRepository<Discount> _discountRepository;
        //private readonly IRepository<BlogPost> _blogPostRepository;
        //private readonly IRepository<Topic> _topicRepository;
        //private readonly IRepository<NewsItem> _newsItemRepository;
        //private readonly IRepository<Poll> _pollRepository;
        //private readonly IRepository<ShippingMethod> _shippingMethodRepository;
        //private readonly IRepository<DeliveryDate> _deliveryDateRepository;
        //private readonly IRepository<ActivityLogType> _activityLogTypeRepository;
        private readonly IRepository<ProductTag> _productTagRepository;
        //private readonly IRepository<ProductTemplate> _productTemplateRepository;
        //private readonly IRepository<CategoryTemplate> _categoryTemplateRepository;
        //private readonly IRepository<ManufacturerTemplate> _manufacturerTemplateRepository;
        //private readonly IRepository<TopicTemplate> _topicTemplateRepository;
        //private readonly IRepository<ScheduleTask> _scheduleTaskRepository;
        //private readonly IGenericAttributeService _genericAttributeService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public CodeFirstInstallationService(IRepository<Store> storeRepository,
            //IRepository<MeasureDimension> measureDimensionRepository,
            //IRepository<MeasureWeight> measureWeightRepository,
            //IRepository<TaxCategory> taxCategoryRepository,
            //IRepository<Language> languageRepository,
            //IRepository<Currency> currencyRepository,
            IRepository<Customer> customerRepository,
            IRepository<CustomerRole> customerRoleRepository,
            //IRepository<SpecificationAttribute> specificationAttributeRepository,
            //IRepository<CheckoutAttribute> checkoutAttributeRepository,
            //IRepository<ProductAttribute> productAttributeRepository,
            IRepository<StoreCategory> categoryRepository,
            //IRepository<Manufacturer> manufacturerRepository,
            IRepository<Product> productRepository,
            //IRepository<UrlRecord> urlRecordRepository,
            //IRepository<RelatedProduct> relatedProductRepository,
            IRepository<EmailAccount> emailAccountRepository,
            IRepository<MessageTemplate> messageTemplateRepository,
            //IRepository<ForumGroup> forumGroupRepository,
            //IRepository<Forum> forumRepository,
            //IRepository<Country> countryRepository,
            //IRepository<StateProvince> stateProvinceRepository,
            //IRepository<Discount> discountRepository,
            //IRepository<BlogPost> blogPostRepository,
            //IRepository<Topic> topicRepository,
            //IRepository<NewsItem> newsItemRepository,
            //IRepository<Poll> pollRepository,
            //IRepository<ShippingMethod> shippingMethodRepository,
            //IRepository<DeliveryDate> deliveryDateRepository,
            //IRepository<ActivityLogType> activityLogTypeRepository,
            IRepository<ProductTag> productTagRepository,
            //IRepository<ProductTemplate> productTemplateRepository,
            //IRepository<CategoryTemplate> categoryTemplateRepository,
            //IRepository<ManufacturerTemplate> manufacturerTemplateRepository,
            //IRepository<TopicTemplate> topicTemplateRepository,
            //IRepository<ScheduleTask> scheduleTaskRepository,
            //IGenericAttributeService genericAttributeService,
            IWebHelper webHelper)
        {
            this._storeRepository = storeRepository;
            //this._measureDimensionRepository = measureDimensionRepository;
            //this._measureWeightRepository = measureWeightRepository;
            //this._taxCategoryRepository = taxCategoryRepository;
            //this._languageRepository = languageRepository;
            //this._currencyRepository = currencyRepository;
            this._customerRepository = customerRepository;
            this._customerRoleRepository = customerRoleRepository;
            //this._specificationAttributeRepository = specificationAttributeRepository;
            //this._checkoutAttributeRepository = checkoutAttributeRepository;
            //this._productAttributeRepository = productAttributeRepository;
            this._categoryRepository = categoryRepository;
            //this._manufacturerRepository = manufacturerRepository;
            this._productRepository = productRepository;
            //this._urlRecordRepository = urlRecordRepository;
            //this._relatedProductRepository = relatedProductRepository;
            this._emailAccountRepository = emailAccountRepository;
            this._messageTemplateRepository = messageTemplateRepository;
            //this._forumGroupRepository = forumGroupRepository;
            //this._forumRepository = forumRepository;
            //this._countryRepository = countryRepository;
            //this._stateProvinceRepository = stateProvinceRepository;
            //this._discountRepository = discountRepository;
            //this._blogPostRepository = blogPostRepository;
            //this._topicRepository = topicRepository;
            //this._newsItemRepository = newsItemRepository;
            //this._pollRepository = pollRepository;
            //this._shippingMethodRepository = shippingMethodRepository;
            //this._deliveryDateRepository = deliveryDateRepository;
            //this._activityLogTypeRepository = activityLogTypeRepository;
            //this._productTagRepository = productTagRepository;
            //this._productTemplateRepository = productTemplateRepository;
            //this._categoryTemplateRepository = categoryTemplateRepository;
            //this._manufacturerTemplateRepository = manufacturerTemplateRepository;
            //this._topicTemplateRepository = topicTemplateRepository;
            //this._scheduleTaskRepository = scheduleTaskRepository;
            //this._genericAttributeService = genericAttributeService;
            this._webHelper = webHelper;
        }

        #endregion

        #region Utilities

        protected virtual void InstallCustomersAndUsers(string defaultUserEmail, string defaultUserPassword)
        {
            var crAdministrators = new CustomerRole
            {
                Name = "Administrators",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemCustomerRoleNames.Administrators,
            };
            var crForumModerators = new CustomerRole
            {
                Name = "Forum Moderators",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemCustomerRoleNames.ForumModerators,
            };
            var crRegistered = new CustomerRole
            {
                Name = "Registered",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemCustomerRoleNames.Registered,
            };
            var crGuests = new CustomerRole
            {
                Name = "Guests",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemCustomerRoleNames.Guests,
            };
            var crvenders = new CustomerRole
            {
                Name = "venders",
                Active = true,
                IsSystemRole = true,
                SystemName = SystemCustomerRoleNames.venders,
            };
            var customerRoles = new List<CustomerRole>
                                {
                                    crAdministrators,
                                    crForumModerators,
                                    crRegistered,
                                    crGuests,
                                    crvenders
                                };
            _customerRoleRepository.Insert(customerRoles);

            //admin user
            var adminUser = new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                Email = defaultUserEmail,
                UserName = defaultUserEmail,
                Password = defaultUserPassword,
                PasswordFormat = PasswordFormat.Clear,
                PasswordSalt = "",
                Active = true,
                CreatedOn = DateTime.UtcNow,
                LastActivityDate = DateTime.UtcNow,
            };
            //var defaultAdminUserAddress = new Address
            //{
            //    FirstName = "John",
            //    LastName = "Smith",
            //    PhoneNumber = "12345678",
            //    Email = "admin@yourStore.com",
            //    FaxNumber = "",
            //    Company = "Nop Solutions",
            //    Address1 = "21 West 52nd Street",
            //    Address2 = "",
            //    City = "New York",
            //    StateProvince = _stateProvinceRepository.Table.FirstOrDefault(sp => sp.Name == "New York"),
            //    Country = _countryRepository.Table.FirstOrDefault(c => c.ThreeLetterIsoCode == "USA"),
            //    ZipPostalCode = "10021",
            //    CreatedOn = DateTime.UtcNow,
            //};
            //adminUser.Addresses.Add(defaultAdminUserAddress);
            //adminUser.BillingAddress = defaultAdminUserAddress;
            //adminUser.ShippingAddress = defaultAdminUserAddress;
            adminUser.CustomerRoles.Add(crAdministrators);
            adminUser.CustomerRoles.Add(crForumModerators);
            adminUser.CustomerRoles.Add(crRegistered);
            _customerRepository.Insert(adminUser);
            //set default customer name
            //_genericAttributeService.SaveAttribute(adminUser, SystemCustomerAttributeNames.FirstName, "John");
            //_genericAttributeService.SaveAttribute(adminUser, SystemCustomerAttributeNames.LastName, "Smith");


            //search engine (crawler) built-in user
            var searchEngineUser = new Customer
            {
                Email = "builtin@search_engine_record.com",
                CustomerGuid = Guid.NewGuid(),
                PasswordFormat = PasswordFormat.Clear,
                AdminComment = "Built-in system guest record used for requests from search engines.",
                Active = true,
                IsSystemAccount = true,
                SystemName = SystemCustomerNames.SearchEngine,
                CreatedOn = DateTime.UtcNow,
                LastActivityDate = DateTime.UtcNow,
            };
            searchEngineUser.CustomerRoles.Add(crGuests);
            _customerRepository.Insert(searchEngineUser);


            //built-in user for background tasks
            var backgroundTaskUser = new Customer
            {
                Email = "builtin@background-task-record.com",
                CustomerGuid = Guid.NewGuid(),
                PasswordFormat = PasswordFormat.Clear,
                AdminComment = "Built-in system record used for background tasks.",
                Active = true,
                IsSystemAccount = true,
                SystemName = SystemCustomerNames.BackgroundTask,
                CreatedOn = DateTime.UtcNow,
                LastActivityDate = DateTime.UtcNow,
            };
            backgroundTaskUser.CustomerRoles.Add(crGuests);
            _customerRepository.Insert(backgroundTaskUser);
        }

        //protected virtual void HashDefaultCustomerPassword(string defaultUserEmail, string defaultUserPassword)
        //{
        //    var customerRegistrationService = EngineContext.Current.Resolve<ICustomerRegistrationService>();
        //    customerRegistrationService.ChangePassword(new ChangePasswordRequest(defaultUserEmail, false,
        //         PasswordFormat.Hashed, defaultUserPassword));
        //}

        protected virtual void InstallEmailAccounts()
        {
            var emailAccounts = new List<EmailAccount>
                               {
                                   new EmailAccount
                                       {
                                           Email = "qmtech@yeah.net",
                                           DisplayName = "Store name",
                                           Host = "smtp.yeah.net",
                                           Port = 25,
                                           Username = "qmtech@yeah.net",
                                           Password = "anglefox4723",
                                           EnableSsl = false,
                                           UseDefaultCredentials = false
                                       },
                               };
            _emailAccountRepository.Insert(emailAccounts);
        }

        protected virtual void InstallMessageTemplates()
        {
            var eaGeneral = _emailAccountRepository.Table.FirstOrDefault();
            if (eaGeneral == null)
                throw new Exception("Default email account cannot be loaded");
            var messageTemplates = new List<MessageTemplate>
                               {
                                   new MessageTemplate
                                       {
                                           Name = "Blog.BlogComment",
                                           Subject = "%Store.Name%. New blog comment.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />A new blog comment has been created for blog post \"%BlogComment.BlogPostTitle%\".</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Customer.BackInStock",
                                           Subject = "%Store.Name%. Back in stock notification",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />Hello %Customer.FullName%, <br />Product <a target=\"_blank\" href=\"%BackInStockSubscription.ProductUrl%\">%BackInStockSubscription.ProductName%</a> is in stock.</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Customer.EmailValidationMessage",
                                           Subject = "%Store.Name%. Email validation",
                                           Body = "<a href=\"%Store.URL%\">%Store.Name%</a>  <br />  <br />  To activate your account <a href=\"%Customer.AccountActivationURL%\">click here</a>.     <br />  <br />  %Store.Name%",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Customer.NewPM",
                                           Subject = "%Store.Name%. You have received a new private message",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />You have received a new private message.</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Customer.PasswordRecovery",
                                           Subject = "%Store.Name%. Password recovery",
                                           Body = "<a href=\"%Store.URL%\">%Store.Name%</a>  <br />  <br />  To change your password <a href=\"%Customer.PasswordRecoveryURL%\">click here</a>.     <br />  <br />  %Store.Name%",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Customer.WelcomeMessage",
                                           Subject = "Welcome to %Store.Name%",
                                           Body = "We welcome you to <a href=\"%Store.URL%\"> %Store.Name%</a>.<br /><br />You can now take part in the various services we have to offer you. Some of these services include:<br /><br />Permanent Cart - Any products added to your online cart remain there until you remove them, or check them out.<br />Address Book - We can now deliver your products to another address other than yours! This is perfect to send birthday gifts direct to the birthday-person themselves.<br />Order History - View your history of purchases that you have made with us.<br />Products Reviews - Share your opinions on products with our other customers.<br /><br />For help with any of our online services, please email the store-owner: <a href=\"mailto:%Store.Email%\">%Store.Email%</a>.<br /><br />Note: This email address was provided on our registration page. If you own the email and did not register on our site, please send an email to <a href=\"mailto:%Store.Email%\">%Store.Email%</a>.",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Forums.NewForumPost",
                                           Subject = "%Store.Name%. New Post Notification.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />A new post has been created in the topic <a href=\"%Forums.TopicURL%\">\"%Forums.TopicName%\"</a> at <a href=\"%Forums.ForumURL%\">\"%Forums.ForumName%\"</a> forum.<br /><br />Click <a href=\"%Forums.TopicURL%\">here</a> for more info.<br /><br />Post author: %Forums.PostAuthor%<br />Post body: %Forums.PostBody%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Forums.NewForumTopic",
                                           Subject = "%Store.Name%. New Topic Notification.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />A new topic <a href=\"%Forums.TopicURL%\">\"%Forums.TopicName%\"</a> has been created at <a href=\"%Forums.ForumURL%\">\"%Forums.ForumName%\"</a> forum.<br /><br />Click <a href=\"%Forums.TopicURL%\">here</a> for more info.</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "GiftCard.Notification",
                                           Subject = "%GiftCard.SenderName% has sent you a gift card for %Store.Name%",
                                           Body = "<p>You have received a gift card for %Store.Name%</p><p>Dear %GiftCard.RecipientName%, <br /><br />%GiftCard.SenderName% (%GiftCard.SenderEmail%) has sent you a %GiftCard.Amount% gift cart for <a href=\"%Store.URL%\"> %Store.Name%</a></p><p>You gift card code is %GiftCard.CouponCode%</p><p>%GiftCard.Message%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "NewCustomer.Notification",
                                           Subject = "%Store.Name%. New customer registration",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />A new customer registered with your store. Below are the customer's details:<br />Full name: %Customer.FullName%<br />Email: %Customer.Email%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "NewReturnRequest.StoreOwnerNotification",
                                           Subject = "%Store.Name%. New return request.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Customer.FullName% has just submitted a new return request. Details are below:<br />Request ID: %ReturnRequest.ID%<br />Product: %ReturnRequest.Product.Quantity% x Product: %ReturnRequest.Product.Name%<br />Reason for return: %ReturnRequest.Reason%<br />Requested action: %ReturnRequest.RequestedAction%<br />Customer comments:<br />%ReturnRequest.CustomerComment%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "News.NewsComment",
                                           Subject = "%Store.Name%. New news comment.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />A new news comment has been created for news \"%NewsComment.NewsTitle%\".</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "NewsLetterSubscription.ActivationMessage",
                                           Subject = "%Store.Name%. Subscription activation message.",
                                           Body = "<p><a href=\"%NewsLetterSubscription.ActivationUrl%\">Click here to confirm your subscription to our list.</a></p><p>If you received this email by mistake, simply delete it.</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "NewsLetterSubscription.DeactivationMessage",
                                           Subject = "%Store.Name%. Subscription deactivation message.",
                                           Body = "<p><a href=\"%NewsLetterSubscription.DeactivationUrl%\">Click here to unsubscribe from our newsletter.</a></p><p>If you received this email by mistake, simply delete it.</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "NewVATSubmitted.StoreOwnerNotification",
                                           Subject = "%Store.Name%. New VAT number is submitted.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Customer.FullName% (%Customer.Email%) has just submitted a new VAT number. Details are below:<br />VAT number: %Customer.VatNumber%<br />VAT number status: %Customer.VatNumberStatus%<br />Received name: %VatValidationResult.Name%<br />Received address: %VatValidationResult.Address%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "OrderCancelled.CustomerNotification",
                                           Subject = "%Store.Name%. Your order cancelled",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />Hello %Order.CustomerFullName%, <br />Your order has been cancelled. Below is the summary of the order. <br /><br />Order Number: %Order.OrderNumber%<br />Order Details: <a target=\"_blank\" href=\"%Order.OrderURLForCustomer%\">%Order.OrderURLForCustomer%</a><br />Date Ordered: %Order.CreatedOn%<br /><br /><br /><br />Billing Address<br />%Order.BillingFirstName% %Order.BillingLastName%<br />%Order.BillingAddress1%<br />%Order.BillingCity% %Order.BillingZipPostalCode%<br />%Order.BillingStateProvince% %Order.BillingCountry%<br /><br /><br /><br />Shipping Address<br />%Order.ShippingFirstName% %Order.ShippingLastName%<br />%Order.ShippingAddress1%<br />%Order.ShippingCity% %Order.ShippingZipPostalCode%<br />%Order.ShippingStateProvince% %Order.ShippingCountry%<br /><br />Shipping Method: %Order.ShippingMethod%<br /><br />%Order.Product(s)%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "OrderCompleted.CustomerNotification",
                                           Subject = "%Store.Name%. Your order completed",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />Hello %Order.CustomerFullName%, <br />Your order has been completed. Below is the summary of the order. <br /><br />Order Number: %Order.OrderNumber%<br />Order Details: <a target=\"_blank\" href=\"%Order.OrderURLForCustomer%\">%Order.OrderURLForCustomer%</a><br />Date Ordered: %Order.CreatedOn%<br /><br /><br /><br />Billing Address<br />%Order.BillingFirstName% %Order.BillingLastName%<br />%Order.BillingAddress1%<br />%Order.BillingCity% %Order.BillingZipPostalCode%<br />%Order.BillingStateProvince% %Order.BillingCountry%<br /><br /><br /><br />Shipping Address<br />%Order.ShippingFirstName% %Order.ShippingLastName%<br />%Order.ShippingAddress1%<br />%Order.ShippingCity% %Order.ShippingZipPostalCode%<br />%Order.ShippingStateProvince% %Order.ShippingCountry%<br /><br />Shipping Method: %Order.ShippingMethod%<br /><br />%Order.Product(s)%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "ShipmentDelivered.CustomerNotification",
                                           Subject = "Your order from %Store.Name% has been delivered.",
                                           Body = "<p><a href=\"%Store.URL%\"> %Store.Name%</a> <br /> <br /> Hello %Order.CustomerFullName%, <br /> Good news! You order has been delivered. <br /> Order Number: %Order.OrderNumber%<br /> Order Details: <a href=\"%Order.OrderURLForCustomer%\" target=\"_blank\">%Order.OrderURLForCustomer%</a><br /> Date Ordered: %Order.CreatedOn%<br /> <br /> <br /> <br /> Billing Address<br /> %Order.BillingFirstName% %Order.BillingLastName%<br /> %Order.BillingAddress1%<br /> %Order.BillingCity% %Order.BillingZipPostalCode%<br /> %Order.BillingStateProvince% %Order.BillingCountry%<br /> <br /> <br /> <br /> Shipping Address<br /> %Order.ShippingFirstName% %Order.ShippingLastName%<br /> %Order.ShippingAddress1%<br /> %Order.ShippingCity% %Order.ShippingZipPostalCode%<br /> %Order.ShippingStateProvince% %Order.ShippingCountry%<br /> <br /> Shipping Method: %Order.ShippingMethod% <br /> <br /> Delivered Products: <br /> <br /> %Shipment.Product(s)%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "OrderPlaced.CustomerNotification",
                                           Subject = "Order receipt from %Store.Name%.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />Hello %Order.CustomerFullName%, <br />Thanks for buying from <a href=\"%Store.URL%\">%Store.Name%</a>. Below is the summary of the order. <br /><br />Order Number: %Order.OrderNumber%<br />Order Details: <a target=\"_blank\" href=\"%Order.OrderURLForCustomer%\">%Order.OrderURLForCustomer%</a><br />Date Ordered: %Order.CreatedOn%<br /><br /><br /><br />Billing Address<br />%Order.BillingFirstName% %Order.BillingLastName%<br />%Order.BillingAddress1%<br />%Order.BillingCity% %Order.BillingZipPostalCode%<br />%Order.BillingStateProvince% %Order.BillingCountry%<br /><br /><br /><br />Shipping Address<br />%Order.ShippingFirstName% %Order.ShippingLastName%<br />%Order.ShippingAddress1%<br />%Order.ShippingCity% %Order.ShippingZipPostalCode%<br />%Order.ShippingStateProvince% %Order.ShippingCountry%<br /><br />Shipping Method: %Order.ShippingMethod%<br /><br />%Order.Product(s)%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "OrderPlaced.StoreOwnerNotification",
                                           Subject = "%Store.Name%. Purchase Receipt for Order #%Order.OrderNumber%",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Order.CustomerFullName% (%Order.CustomerEmail%) has just placed an order from your store. Below is the summary of the order. <br /><br />Order Number: %Order.OrderNumber%<br />Date Ordered: %Order.CreatedOn%<br /><br /><br /><br />Billing Address<br />%Order.BillingFirstName% %Order.BillingLastName%<br />%Order.BillingAddress1%<br />%Order.BillingCity% %Order.BillingZipPostalCode%<br />%Order.BillingStateProvince% %Order.BillingCountry%<br /><br /><br /><br />Shipping Address<br />%Order.ShippingFirstName% %Order.ShippingLastName%<br />%Order.ShippingAddress1%<br />%Order.ShippingCity% %Order.ShippingZipPostalCode%<br />%Order.ShippingStateProvince% %Order.ShippingCountry%<br /><br />Shipping Method: %Order.ShippingMethod%<br /><br />%Order.Product(s)%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "ShipmentSent.CustomerNotification",
                                           Subject = "Your order from %Store.Name% has been shipped.",
                                           Body = "<p><a href=\"%Store.URL%\"> %Store.Name%</a> <br /><br />Hello %Order.CustomerFullName%!, <br />Good news! You order has been shipped. <br />Order Number: %Order.OrderNumber%<br />Order Details: <a href=\"%Order.OrderURLForCustomer%\" target=\"_blank\">%Order.OrderURLForCustomer%</a><br />Date Ordered: %Order.CreatedOn%<br /><br /><br /><br />Billing Address<br />%Order.BillingFirstName% %Order.BillingLastName%<br />%Order.BillingAddress1%<br />%Order.BillingCity% %Order.BillingZipPostalCode%<br />%Order.BillingStateProvince% %Order.BillingCountry%<br /><br /><br /><br />Shipping Address<br />%Order.ShippingFirstName% %Order.ShippingLastName%<br />%Order.ShippingAddress1%<br />%Order.ShippingCity% %Order.ShippingZipPostalCode%<br />%Order.ShippingStateProvince% %Order.ShippingCountry%<br /><br />Shipping Method: %Order.ShippingMethod% <br /> <br /> Shipped Products: <br /> <br /> %Shipment.Product(s)%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Product.ProductReview",
                                           Subject = "%Store.Name%. New product review.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />A new product review has been written for product \"%ProductReview.ProductName%\".</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "QuantityBelow.StoreOwnerNotification",
                                           Subject = "%Store.Name%. Quantity below notification. %Product.Name%",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Product.Name% (ID: %Product.ID%) low quantity. <br /><br />Quantity: %Product.StockQuantity%<br /></p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "QuantityBelow.AttributeCombination.StoreOwnerNotification",
                                           Subject = "%Store.Name%. Quantity below notification. %Product.Name%",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Product.Name% (ID: %Product.ID%) low quantity. <br />%AttributeCombination.Formatted%<br />Quantity: %AttributeCombination.StockQuantity%<br /></p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "ReturnRequestStatusChanged.CustomerNotification",
                                           Subject = "%Store.Name%. Return request status was changed.",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />Hello %Customer.FullName%,<br />Your return request #%ReturnRequest.ID% status has been changed.</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Service.EmailAFriend",
                                           Subject = "%Store.Name%. Referred Item",
                                           Body = "<p><a href=\"%Store.URL%\"> %Store.Name%</a> <br /><br />%EmailAFriend.Email% was shopping on %Store.Name% and wanted to share the following item with you. <br /><br /><b><a target=\"_blank\" href=\"%Product.ProductURLForCustomer%\">%Product.Name%</a></b> <br />%Product.ShortDescription% <br /><br />For more info click <a target=\"_blank\" href=\"%Product.ProductURLForCustomer%\">here</a> <br /><br /><br />%EmailAFriend.PersonalMessage%<br /><br />%Store.Name%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Wishlist.EmailAFriend",
                                           Subject = "%Store.Name%. Wishlist",
                                           Body = "<p><a href=\"%Store.URL%\"> %Store.Name%</a> <br /><br />%Wishlist.Email% was shopping on %Store.Name% and wanted to share a wishlist with you. <br /><br /><br />For more info click <a target=\"_blank\" href=\"%Wishlist.URLForCustomer%\">here</a> <br /><br /><br />%Wishlist.PersonalMessage%<br /><br />%Store.Name%</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "Customer.NewOrderNote",
                                           Subject = "%Store.Name%. New order note has been added",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />Hello %Customer.FullName%, <br />New order note has been added to your account:<br />\"%Order.NewNoteText%\".<br /><a target=\"_blank\" href=\"%Order.OrderURLForCustomer%\">%Order.OrderURLForCustomer%</a></p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "RecurringPaymentCancelled.StoreOwnerNotification",
                                           Subject = "%Store.Name%. Recurring payment cancelled",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Customer.FullName% (%Customer.Email%) has just cancelled a recurring payment ID=%RecurringPayment.ID%.</p>",
                                           IsActive = true,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "OrderPlaced.venderNotification",
                                           Subject = "%Store.Name%. Order placed",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />%Customer.FullName% (%Customer.Email%) has just placed an order. <br /><br />Order Number: %Order.OrderNumber%<br />Date Ordered: %Order.CreatedOn%<br /><br />%Order.Product(s)%</p>",
                                           //this template is disabled by default
                                           IsActive = false,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "OrderPaid.StoreOwnerNotification",
                                           Subject = "%Store.Name%. Order #%Order.OrderNumber% paid",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />Order #%Order.OrderNumber% has been just paid<br />Date Ordered: %Order.CreatedOn%</p>",
                                           //this template is disabled by default
                                           IsActive = false,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "OrderPaid.CustomerNotification",
                                           Subject = "%Store.Name%. Order #%Order.OrderNumber% paid",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />Hello %Order.CustomerFullName%, <br />Thanks for buying from <a href=\"%Store.URL%\">%Store.Name%</a>. Order #%Order.OrderNumber% has been just paid. Below is the summary of the order. <br /><br />Order Number: %Order.OrderNumber%<br />Order Details: <a href=\"%Order.OrderURLForCustomer%\" target=\"_blank\">%Order.OrderURLForCustomer%</a><br />Date Ordered: %Order.CreatedOn%<br /><br /><br /><br />Billing Address<br />%Order.BillingFirstName% %Order.BillingLastName%<br />%Order.BillingAddress1%<br />%Order.BillingCity% %Order.BillingZipPostalCode%<br />%Order.BillingStateProvince% %Order.BillingCountry%<br /><br /><br /><br />Shipping Address<br />%Order.ShippingFirstName% %Order.ShippingLastName%<br />%Order.ShippingAddress1%<br />%Order.ShippingCity% %Order.ShippingZipPostalCode%<br />%Order.ShippingStateProvince% %Order.ShippingCountry%<br /><br />Shipping Method: %Order.ShippingMethod%<br /><br />%Order.Product(s)%</p>",
                                           //this template is disabled by default
                                           IsActive = false,
                                           EmailAccountId = eaGeneral.Id,
                                       },
                                   new MessageTemplate
                                       {
                                           Name = "OrderPaid.venderNotification",
                                           Subject = "%Store.Name%. Order #%Order.OrderNumber% paid",
                                           Body = "<p><a href=\"%Store.URL%\">%Store.Name%</a> <br /><br />Order #%Order.OrderNumber% has been just paid. <br /><br />Order Number: %Order.OrderNumber%<br />Date Ordered: %Order.CreatedOn%<br /><br />%Order.Product(s)%</p>",
                                           //this template is disabled by default
                                           IsActive = false,
                                           EmailAccountId = eaGeneral.Id,
                                       }
                               };
            _messageTemplateRepository.Insert(messageTemplates);
        }

        //protected virtual void InstallTopics()
        //{
        //    var defaultTopicTemplate =
        //        _topicTemplateRepository.Table.FirstOrDefault(tt => tt.Name == "Default template");
        //    if (defaultTopicTemplate == null)
        //        throw new Exception("Topic template cannot be loaded");

        //    var topics = new List<Topic>
        //                       {
        //                           new Topic
        //                               {
        //                                   SystemName = "AboutUs",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   IncludeInFooterColumn1 = true,
        //                                   DisplayOrder = 20,
        //                                   Title = "About us",
        //                                   Body = "<p>Put your &quot;About Us&quot; information here. You can edit this in the admin site.</p>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                           new Topic
        //                               {
        //                                   SystemName = "CheckoutAsGuestOrRegister",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   DisplayOrder = 1,
        //                                   Title = "",
        //                                   Body = "<p><strong>Register and save time!</strong><br />Register with us for future convenience:</p><ul><li>Fast and easy check out</li><li>Easy access to your order history and status</li></ul>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                           new Topic
        //                               {
        //                                   SystemName = "ConditionsOfUse",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   IncludeInFooterColumn1 = true,
        //                                   DisplayOrder = 15,
        //                                   Title = "Conditions of Use",
        //                                   Body = "<p>Put your conditions of use information here. You can edit this in the admin site.</p>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                           new Topic
        //                               {
        //                                   SystemName = "ContactUs",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   DisplayOrder = 1,
        //                                   Title = "",
        //                                   Body = "<p>Put your contact information here. You can edit this in the admin site.</p>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                           new Topic
        //                               {
        //                                   SystemName = "ForumWelcomeMessage",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   DisplayOrder = 1,
        //                                   Title = "Forums",
        //                                   Body = "<p>Put your welcome message here. You can edit this in the admin site.</p>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                           new Topic
        //                               {
        //                                   SystemName = "HomePageText",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   DisplayOrder = 1,
        //                                   Title = "Welcome to our store",
        //                                   Body = "<p>Online shopping is the process consumers go through to purchase products or services over the Internet. You can edit this in the admin site.</p><p>If you have questions, see the <a href=\"http://www.nopcommerce.com/documentation.aspx\">Documentation</a>, or post in the <a href=\"http://www.nopcommerce.com/boards/\">Forums</a> at <a href=\"http://www.nopcommerce.com\">nopCommerce.com</a></p>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                           new Topic
        //                               {
        //                                   SystemName = "LoginRegistrationInfo",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   DisplayOrder = 1,
        //                                   Title = "About login / registration",
        //                                   Body = "<p>Put your login / registration information here. You can edit this in the admin site.</p>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                           new Topic
        //                               {
        //                                   SystemName = "PrivacyInfo",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   IncludeInFooterColumn1 = true,
        //                                   DisplayOrder = 10,
        //                                   Title = "Privacy notice",
        //                                   Body = "<p>Put your privacy policy information here. You can edit this in the admin site.</p>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                           new Topic
        //                               {
        //                                   SystemName = "PageNotFound",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   DisplayOrder = 1,
        //                                   Title = "",
        //                                   Body = "<p><strong>The page you requested was not found, and we have a fine guess why.</strong></p><ul><li>If you typed the URL directly, please make sure the spelling is correct.</li><li>The page no longer exists. In this case, we profusely apologize for the inconvenience and for any damage this may cause.</li></ul>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                           new Topic
        //                               {
        //                                   SystemName = "ShippingInfo",
        //                                   IncludeInSitemap = false,
        //                                   IsPasswordProtected = false,
        //                                   IncludeInFooterColumn1 = true,
        //                                   DisplayOrder = 5,
        //                                   Title = "Shipping & returns",
        //                                   Body = "<p>Put your shipping &amp; returns information here. You can edit this in the admin site.</p>",
        //                                   TopicTemplateId = defaultTopicTemplate.Id
        //                               },
        //                       };
        //    _topicRepository.Insert(topics);


        //    //search engine names
        //    foreach (var topic in topics)
        //    {
        //        _urlRecordRepository.Insert(new UrlRecord
        //        {
        //            EntityId = topic.Id,
        //            EntityName = "Topic",
        //            LanguageId = 0,
        //            IsActive = true,
        //            Slug = topic.ValidateSeName("", !String.IsNullOrEmpty(topic.Title) ? topic.Title : topic.SystemName, true)
        //        });
        //    }

        //}

        protected virtual void InstallSettings()
        {
            var settingService = EngineContext.Current.Resolve<ISettingService>();
            //settingService.SaveSetting(new PdfSettings
            //{
            //    LogoPictureId = 0,
            //    LetterPageSizeEnabled = false,
            //    RenderOrderNotes = true,
            //    FontFileName = "FreeSerif.ttf",
            //    InvoiceFooterTextColumn1 = null,
            //    InvoiceFooterTextColumn2 = null,
            //});

            settingService.SaveSetting(new CommonSettings
            {
                UseSystemEmailForContactUsForm = true,
                UseStoredProceduresIfSupported = true,
                SitemapEnabled = true,
                SitemapIncludeCategories = true,
                SitemapIncludeManufacturers = true,
                SitemapIncludeProducts = false,
                DisplayJavaScriptDisabledWarning = false,
                UseFullTextSearch = false,
                FullTextMode = FulltextSearchMode.ExactMatch,
                Log404Errors = true,
                BreadcrumbDelimiter = "/",
                RenderXuaCompatible = false,
                XuaCompatibleValue = "IE=edge"
            });

            //settingService.SaveSetting(new SeoSettings
            //{
            //    PageTitleSeparator = ". ",
            //    PageTitleSeoAdjustment = PageTitleSeoAdjustment.PagenameAfterStorename,
            //    DefaultTitle = "Your store",
            //    DefaultMetaKeywords = "",
            //    DefaultMetaDescription = "",
            //    GenerateProductMetaDescription = true,
            //    ConvertNonWesternChars = false,
            //    AllowUnicodeCharsInUrls = true,
            //    CanonicalUrlsEnabled = false,
            //    WwwRequirement = WwwRequirement.NoMatter,
            //    //we disable bundling out of the box because it requires a lot of server resources
            //    EnableJsBundling = false,
            //    EnableCssBundling = false,
            //    TwitterMetaTags = true,
            //    OpenGraphMetaTags = true,
            //    ReservedUrlRecordSlugs = new List<string>
            //        {
            //            "admin",
            //            "install",
            //            "recentlyviewedproducts",
            //            "newproducts",
            //            "compareproducts",
            //            "clearcomparelist",
            //            "setproductreviewhelpfulness",
            //            "login",
            //            "register",
            //            "logout",
            //            "cart",
            //            "wishlist",
            //            "emailwishlist",
            //            "checkout",
            //            "onepagecheckout",
            //            "contactus",
            //            "passwordrecovery",
            //            "subscribenewsletter",
            //            "blog",
            //            "boards",
            //            "inboxupdate",
            //            "sentupdate",
            //            "news",
            //            "sitemap",
            //            "search",
            //            "config",
            //            "eucookielawaccept",
            //            "page-not-found"
            //        },
            //});

            //settingService.SaveSetting(new AdminAreaSettings
            //{
            //    DefaultGridPageSize = 15,
            //    GridPageSizes = "10, 15, 20, 50, 100",
            //    RichEditorAdditionalSettings = null,
            //    RichEditorAllowJavaScript = false
            //});

            settingService.SaveSetting(new CatalogSettings
            {
                ShowProductSku = false,
                ShowManufacturerPartNumber = false,
                ShowGtin = false,
                ShowFreeShippingNotification = true,
                AllowProductSorting = true,
                AllowProductViewModeChanging = true,
                DefaultViewMode = "grid",
                ShowProductsFromSubcategories = true,
                ShowCategoryProductNumber = false,
                ShowCategoryProductNumberIncludingSubcategories = false,
                CategoryBreadcrumbEnabled = true,
                ShowShareButton = true,
                PageShareCode = "<!-- AddThis Button BEGIN --><div class=\"addthis_toolbox addthis_default_style \"><a class=\"addthis_button_preferred_1\"></a><a class=\"addthis_button_preferred_2\"></a><a class=\"addthis_button_preferred_3\"></a><a class=\"addthis_button_preferred_4\"></a><a class=\"addthis_button_compact\"></a><a class=\"addthis_counter addthis_bubble_style\"></a></div><script type=\"text/javascript\" src=\"http://s7.addthis.com/js/250/addthis_widget.js#pubid=nopsolutions\"></script><!-- AddThis Button END -->",
                ProductReviewsMustBeApproved = false,
                DefaultProductRatingValue = 5,
                AllowAnonymousUsersToReviewProduct = false,
                NotifyStoreOwnerAboutNewProductReviews = false,
                EmailAFriendEnabled = true,
                AllowAnonymousUsersToEmailAFriend = false,
                RecentlyViewedProductsNumber = 3,
                RecentlyViewedProductsEnabled = true,
                RecentlyAddedProductsNumber = 6,
                RecentlyAddedProductsEnabled = true,
                CompareProductsEnabled = true,
                CompareProductsNumber = 4,
                ProductSearchAutoCompleteEnabled = true,
                ProductSearchAutoCompleteNumberOfProducts = 10,
                ProductSearchTermMinimumLength = 3,
                ShowProductImagesInSearchAutoComplete = false,
                ShowBestsellersOnHomepage = false,
                NumberOfBestsellersOnHomepage = 4,
                SearchPageProductsPerPage = 6,
                SearchPageAllowCustomersToSelectPageSize = true,
                SearchPagePageSizeOptions = "6, 3, 9, 18",
                ProductsAlsoPurchasedEnabled = true,
                ProductsAlsoPurchasedNumber = 3,
                EnableDynamicPriceUpdate = true,
                DynamicPriceUpdateAjax = true,
                NumberOfProductTags = 15,
                ProductsByTagPageSize = 4,
                IncludeShortDescriptionInCompareProducts = false,
                IncludeFullDescriptionInCompareProducts = false,
                IncludeFeaturedProductsInNormalLists = false,
                DisplayTierPricesWithDiscounts = true,
                IgnoreDiscounts = false,
                IgnoreFeaturedProducts = false,
                IgnoreAcl = true,
                IgnoreStoreLimitations = true,
                CacheProductPrices = false,
                ProductsByTagAllowCustomersToSelectPageSize = true,
                ProductsByTagPageSizeOptions = "6, 3, 9, 18",
                MaximumBackInStockSubscriptions = 200,
                ManufacturersBlockItemsToDisplay = 2,
                DisplayTaxShippingInfoFooter = false,
                DisplayTaxShippingInfoProductDetailsPage = false,
                DisplayTaxShippingInfoProductBoxes = false,
                DisplayTaxShippingInfoWishlist = false,
                DisplayTaxShippingInfoOrderDetailsPage = false,
                DefaultCategoryPageSizeOptions = "6, 3, 9",
                DefaultCategoryPageSize = 6,
                DefaultManufacturerPageSizeOptions = "6, 3, 9",
                DefaultManufacturerPageSize = 6,
            });

            settingService.SaveSetting(new CustomerSettings
            {
                UsernamesEnabled = false,
                CheckUsernameAvailabilityEnabled = false,
                AllowUsersToChangeUsernames = false,
                DefaultPasswordFormat = PasswordFormat.Hashed,
                HashedPasswordFormat = "SHA1",
                PasswordMinLength = 6,
                PasswordRecoveryLinkDaysValid = 7,
                UserRegistrationType = UserRegistrationType.Standard,
                //AllowCustomersToUploadAvatars = false,
                //AvatarMaximumSizeBytes = 20000,
                //DefaultAvatarEnabled = true,
                //ShowCustomersLocation = false,
                //ShowCustomersJoinDate = false,
                //AllowViewingProfiles = false,
                //NotifyNewCustomerRegistration = false,
                //HideDownloadableProductsTab = false,
                //HideBackInStockSubscriptionsTab = false,
                //DownloadableProductsValidateUser = false,
                //CustomerNameFormat = CustomerNameFormat.ShowFirstName,
                //GenderEnabled = true,
                //DateOfBirthEnabled = true,
                //CompanyEnabled = true,
                //StreetAddressEnabled = false,
                //StreetAddress2Enabled = false,
                //ZipPostalCodeEnabled = false,
                //CityEnabled = false,
                //CountryEnabled = false,
                //CountryRequired = false,
                //StateProvinceEnabled = false,
                //StateProvinceRequired = false,
                //PhoneEnabled = false,
                //FaxEnabled = false,
                //AcceptPrivacyPolicyEnabled = false,
                //NewsletterEnabled = true,
                //NewsletterTickedByDefault = true,
                //HideNewsletterBlock = false,
                //NewsletterBlockAllowToUnsubscribe = false,
                //OnlineCustomerMinutes = 20,
                StoreLastVisitedPage = false,
                //SuffixDeletedCustomers = false,
            });

            //settingService.SaveSetting(new AddressSettings
            //{
            //    CompanyEnabled = true,
            //    StreetAddressEnabled = true,
            //    StreetAddressRequired = true,
            //    StreetAddress2Enabled = true,
            //    ZipPostalCodeEnabled = true,
            //    ZipPostalCodeRequired = true,
            //    CityEnabled = true,
            //    CityRequired = true,
            //    CountryEnabled = true,
            //    StateProvinceEnabled = true,
            //    PhoneEnabled = true,
            //    PhoneRequired = true,
            //    FaxEnabled = true,
            //});

            //settingService.SaveSetting(new MediaSettings
            //{
            //    AvatarPictureSize = 120,
            //    ProductThumbPictureSize = 415,
            //    ProductDetailsPictureSize = 550,
            //    ProductThumbPictureSizeOnProductDetailsPage = 100,
            //    AssociatedProductPictureSize = 220,
            //    CategoryThumbPictureSize = 450,
            //    ManufacturerThumbPictureSize = 420,
            //    CartThumbPictureSize = 80,
            //    MiniCartThumbPictureSize = 70,
            //    AutoCompleteSearchThumbPictureSize = 20,
            //    MaximumImageSize = 1980,
            //    DefaultPictureZoomEnabled = false,
            //    DefaultImageQuality = 80,
            //    MultipleThumbDirectories = false
            //});

            //settingService.SaveSetting(new StoreInformationSettings
            //{
            //    StoreClosed = false,
            //    StoreClosedAllowForAdmins = false,
            //    DefaultStoreTheme = "DefaultClean",
            //    AllowCustomerToSelectTheme = true,
            //    DisplayMiniProfilerInPublicStore = false,
            //    DisplayEuCookieLawWarning = false,
            //    FacebookLink = "http://www.facebook.com/nopCommerce",
            //    TwitterLink = "https://twitter.com/nopCommerce",
            //    YoutubeLink = "http://www.youtube.com/user/nopCommerce",
            //    GooglePlusLink = "https://plus.google.com/+nopcommerce",
            //});

            //settingService.SaveSetting(new ExternalAuthenticationSettings
            //{
            //    AutoRegisterEnabled = true,
            //    RequireEmailValidation = false
            //});

            //settingService.SaveSetting(new RewardPointsSettings
            //{
            //    Enabled = true,
            //    ExchangeRate = 1,
            //    PointsForRegistration = 0,
            //    PointsForPurchases_Amount = 10,
            //    PointsForPurchases_Points = 1,
            //    PointsForPurchases_Awarded = OrderStatus.Complete,
            //    PointsForPurchases_Canceled = OrderStatus.Cancelled,
            //    DisplayHowMuchWillBeEarned = true,
            //});

            //settingService.SaveSetting(new CurrencySettings
            //{
            //    DisplayCurrencyLabel = false,
            //    PrimaryStoreCurrencyId = _currencyRepository.Table.Single(c => c.CurrencyCode == "USD").Id,
            //    PrimaryExchangeRateCurrencyId = _currencyRepository.Table.Single(c => c.CurrencyCode == "USD").Id,
            //    ActiveExchangeRateProviderSystemName = "CurrencyExchange.MoneyConverter",
            //    AutoUpdateEnabled = false,
            //    LastUpdateTime = 0
            //});

            //settingService.SaveSetting(new MeasureSettings
            //{
            //    BaseDimensionId = _measureDimensionRepository.Table.Single(m => m.SystemKeyword == "inches").Id,
            //    BaseWeightId = _measureWeightRepository.Table.Single(m => m.SystemKeyword == "lb").Id,
            //});

            settingService.SaveSetting(new MessageTemplatesSettings
            {
                CaseInvariantReplacement = false,
                Color1 = "#b9babe",
                Color2 = "#ebecee",
                Color3 = "#dde2e6",
            });

            //settingService.SaveSetting(new ShoppingCartSettings
            //{
            //    DisplayCartAfterAddingProduct = false,
            //    DisplayWishlistAfterAddingProduct = false,
            //    MaximumShoppingCartItems = 1000,
            //    MaximumWishlistItems = 1000,
            //    AllowOutOfStockItemsToBeAddedToWishlist = false,
            //    MoveItemsFromWishlistToCart = true,
            //    ShowProductImagesOnShoppingCart = true,
            //    ShowProductImagesOnWishList = true,
            //    ShowDiscountBox = true,
            //    ShowGiftCardBox = true,
            //    CrossSellsNumber = 4,
            //    EmailWishlistEnabled = true,
            //    AllowAnonymousUsersToEmailWishlist = false,
            //    MiniShoppingCartEnabled = true,
            //    ShowProductImagesInMiniShoppingCart = true,
            //    MiniShoppingCartProductNumber = 5,
            //    RoundPricesDuringCalculation = true,
            //    GroupTierPricesForDistinctShoppingCartItems = false,
            //    AllowCartItemEditing = true,
            //    RenderAssociatedAttributeValueQuantity = false
            //});

            //settingService.SaveSetting(new OrderSettings
            //{
            //    IsReOrderAllowed = true,
            //    MinOrderSubtotalAmount = 0,
            //    MinOrderTotalAmount = 0,
            //    AnonymousCheckoutAllowed = true,
            //    TermsOfServiceOnShoppingCartPage = true,
            //    TermsOfServiceOnOrderConfirmPage = false,
            //    OnePageCheckoutEnabled = true,
            //    OnePageCheckoutDisplayOrderTotalsOnPaymentInfoTab = false,
            //    DisableBillingAddressCheckoutStep = false,
            //    DisableOrderCompletedPage = false,
            //    AttachPdfInvoiceToOrderPlacedEmail = false,
            //    AttachPdfInvoiceToOrderCompletedEmail = false,
            //    AttachPdfInvoiceToOrderPaidEmail = false,
            //    ReturnRequestsEnabled = true,
            //    ReturnRequestActions = new List<string> { "Repair", "Replacement", "Store Credit" },
            //    ReturnRequestReasons = new List<string> { "Received Wrong Product", "Wrong Product Ordered", "There Was A Problem With The Product" },
            //    NumberOfDaysReturnRequestAvailable = 365,
            //    MinimumOrderPlacementInterval = 30,
            //});

            settingService.SaveSetting(new SecuritySettings
            {
                ForceSslForAllPages = false,
                EncryptionKey = CommonHelper.GenerateRandomDigitCode(16),
                AdminAreaAllowedIpAddresses = null,
                EnableXsrfProtectionForAdminArea = true,
                //EnableXsrfProtectionForPublicStore = true,
                //HoneypotEnabled = false,
                //HoneypotInputName = "hpinput"
            });

            //settingService.SaveSetting(new ShippingSettings
            //{
            //    ActiveShippingRateComputationMethodSystemNames = new List<string> { "Shipping.FixedRate" },
            //    AllowPickUpInStore = true,
            //    PickUpInStoreFee = decimal.Zero,
            //    UseWarehouseLocation = false,
            //    NotifyCustomerAboutShippingFromMultipleLocations = false,
            //    FreeShippingOverXEnabled = false,
            //    FreeShippingOverXValue = decimal.Zero,
            //    FreeShippingOverXIncludingTax = false,
            //    EstimateShippingEnabled = true,
            //    DisplayShipmentEventsToCustomers = false,
            //    DisplayShipmentEventsToStoreOwner = false,
            //    ReturnValidOptionsIfThereAreAny = true,
            //    BypassShippingMethodSelectionIfOnlyOne = false,
            //    UseCubeRootMethod = true
            //});

            //settingService.SaveSetting(new PaymentSettings
            //{
            //    ActivePaymentMethodSystemNames = new List<string>
            //        {
            //            "Payments.CheckMoneyOrder",
            //            "Payments.Manual",
            //            "Payments.PayInStore",
            //            "Payments.PurchaseOrder",
            //        },
            //    AllowRePostingPayments = true,
            //    BypassPaymentMethodSelectionIfOnlyOne = true,
            //});

            //settingService.SaveSetting(new TaxSettings
            //{
            //    TaxBasedOn = TaxBasedOn.BillingAddress,
            //    TaxDisplayType = TaxDisplayType.ExcludingTax,
            //    ActiveTaxProviderSystemName = "Tax.FixedRate",
            //    DefaultTaxAddressId = 0,
            //    DisplayTaxSuffix = false,
            //    DisplayTaxRates = false,
            //    PricesIncludeTax = false,
            //    AllowCustomersToSelectTaxDisplayType = false,
            //    ForceTaxExclusionFromOrderSubtotal = false,
            //    HideZeroTax = false,
            //    HideTaxInOrderSummary = false,
            //    ShippingIsTaxable = false,
            //    ShippingPriceIncludesTax = false,
            //    ShippingTaxClassId = 0,
            //    PaymentMethodAdditionalFeeIsTaxable = false,
            //    PaymentMethodAdditionalFeeIncludesTax = false,
            //    PaymentMethodAdditionalFeeTaxClassId = 0,
            //    EuVatEnabled = false,
            //    EuVatShopCountryId = 0,
            //    EuVatAllowVatExemption = true,
            //    EuVatUseWebService = false,
            //    EuVatAssumeValid = false,
            //    EuVatEmailAdminWhenNewVatSubmitted = false
            //});

            //settingService.SaveSetting(new DateTimeSettings
            //{
            //    DefaultStoreTimeZoneId = "",
            //    AllowCustomersToSetTimeZone = false
            //});

            //settingService.SaveSetting(new BlogSettings
            //{
            //    Enabled = true,
            //    PostsPageSize = 10,
            //    AllowNotRegisteredUsersToLeaveComments = true,
            //    NotifyAboutNewBlogComments = false,
            //    NumberOfTags = 15,
            //    ShowHeaderRssUrl = false,
            //});
            //settingService.SaveSetting(new NewsSettings
            //{
            //    Enabled = true,
            //    AllowNotRegisteredUsersToLeaveComments = true,
            //    NotifyAboutNewNewsComments = false,
            //    ShowNewsOnMainPage = true,
            //    MainPageNewsCount = 3,
            //    NewsArchivePageSize = 10,
            //    ShowHeaderRssUrl = false,
            //});

            //settingService.SaveSetting(new ForumSettings
            //{
            //    ForumsEnabled = false,
            //    RelativeDateTimeFormattingEnabled = true,
            //    AllowCustomersToDeletePosts = false,
            //    AllowCustomersToEditPosts = false,
            //    AllowCustomersToManageSubscriptions = false,
            //    AllowGuestsToCreatePosts = false,
            //    AllowGuestsToCreateTopics = false,
            //    TopicSubjectMaxLength = 450,
            //    PostMaxLength = 4000,
            //    StrippedTopicMaxLength = 45,
            //    TopicsPageSize = 10,
            //    PostsPageSize = 10,
            //    SearchResultsPageSize = 10,
            //    ActiveDiscussionsPageSize = 50,
            //    LatestCustomerPostsPageSize = 10,
            //    ShowCustomersPostCount = true,
            //    ForumEditor = EditorType.BBCodeEditor,
            //    SignaturesEnabled = true,
            //    AllowPrivateMessages = false,
            //    ShowAlertForPM = false,
            //    PrivateMessagesPageSize = 10,
            //    ForumSubscriptionsPageSize = 10,
            //    NotifyAboutPrivateMessages = false,
            //    PMSubjectMaxLength = 450,
            //    PMTextMaxLength = 4000,
            //    HomePageActiveDiscussionsTopicCount = 5,
            //    ActiveDiscussionsFeedEnabled = false,
            //    ActiveDiscussionsFeedCount = 25,
            //    ForumFeedsEnabled = false,
            //    ForumFeedCount = 10,
            //    ForumSearchTermMinimumLength = 3,
            //});

            //settingService.SaveSetting(new venderSettings
            //{
            //    DefaultvenderPageSizeOptions = "6, 3, 9",
            //    vendersBlockItemsToDisplay = 0,
            //    ShowvenderOnProductDetailsPage = true,
            //    AllowCustomersToContactvenders = true
            //});

            var eaGeneral = _emailAccountRepository.Table.FirstOrDefault();
            if (eaGeneral == null)
                throw new Exception("Default email account cannot be loaded");
            settingService.SaveSetting(new EmailAccountSettings
            {
                DefaultEmailAccountId = eaGeneral.Id
            });

            //settingService.SaveSetting(new WidgetSettings
            //{
            //    ActiveWidgetSystemNames = new List<string> { "Widgets.NivoSlider" },
            //});
        }

        //protected virtual void InstallCheckoutAttributes()
        //{
        //    var ca1 = new CheckoutAttribute
        //    {
        //        Name = "Gift wrapping",
        //        IsRequired = true,
        //        ShippableProductRequired = true,
        //        AttributeControlType = AttributeControlType.DropdownList,
        //        DisplayOrder = 1,
        //    };
        //    ca1.CheckoutAttributeValues.Add(new CheckoutAttributeValue
        //    {
        //        Name = "No",
        //        PriceAdjustment = 0,
        //        DisplayOrder = 1,
        //        IsPreSelected = true,
        //    });
        //    ca1.CheckoutAttributeValues.Add(new CheckoutAttributeValue
        //    {
        //        Name = "Yes",
        //        PriceAdjustment = 10,
        //        DisplayOrder = 2,
        //    });
        //    var checkoutAttributes = new List<CheckoutAttribute>
        //                        {
        //                            ca1,
        //                        };
        //    _checkoutAttributeRepository.Insert(checkoutAttributes);
        //}

        //protected virtual void InstallSpecificationAttributes()
        //{
        //    var sa1 = new SpecificationAttribute
        //    {
        //        Name = "Screensize",
        //        DisplayOrder = 1,
        //    };
        //    sa1.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "13.0''",
        //        DisplayOrder = 2,
        //    });
        //    sa1.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "13.3''",
        //        DisplayOrder = 3,
        //    });
        //    sa1.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "14.0''",
        //        DisplayOrder = 4,
        //    });
        //    sa1.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "15.0''",
        //        DisplayOrder = 4,
        //    });
        //    sa1.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "15.6''",
        //        DisplayOrder = 5,
        //    });
        //    var sa2 = new SpecificationAttribute
        //    {
        //        Name = "CPU Type",
        //        DisplayOrder = 2,
        //    };
        //    sa2.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "Intel Core i5",
        //        DisplayOrder = 1,
        //    });
        //    sa2.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "Intel Core i7",
        //        DisplayOrder = 2,
        //    });
        //    var sa3 = new SpecificationAttribute
        //    {
        //        Name = "Memory",
        //        DisplayOrder = 3,
        //    };
        //    sa3.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "4 GB",
        //        DisplayOrder = 1,
        //    });
        //    sa3.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "8 GB",
        //        DisplayOrder = 2,
        //    });
        //    sa3.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "16 GB",
        //        DisplayOrder = 3,
        //    });
        //    var sa4 = new SpecificationAttribute
        //    {
        //        Name = "Hardrive",
        //        DisplayOrder = 5,
        //    };
        //    sa4.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "128 GB",
        //        DisplayOrder = 7,
        //    });
        //    sa4.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "500 GB",
        //        DisplayOrder = 4,
        //    });
        //    sa4.SpecificationAttributeOptions.Add(new SpecificationAttributeOption
        //    {
        //        Name = "1 TB",
        //        DisplayOrder = 3,
        //    });
        //    var specificationAttributes = new List<SpecificationAttribute>
        //                        {
        //                            sa1,
        //                            sa2,
        //                            sa3,
        //                            sa4
        //                        };
        //    _specificationAttributeRepository.Insert(specificationAttributes);
        //}

        //protected virtual void InstallProductAttributes()
        //{
        //    var productAttributes = new List<ProductAttribute>
        //    {
        //        new ProductAttribute
        //        {
        //            Name = "Color",
        //        },
        //        new ProductAttribute
        //        {
        //            Name = "Custom Text",
        //        },
        //        new ProductAttribute
        //        {
        //            Name = "HDD",
        //        },
        //        new ProductAttribute
        //        {
        //            Name = "OS",
        //        },
        //        new ProductAttribute
        //        {
        //            Name = "Processor",
        //        },
        //        new ProductAttribute
        //        {
        //            Name = "RAM",
        //        },
        //        new ProductAttribute
        //        {
        //            Name = "Size",
        //        },
        //        new ProductAttribute
        //        {
        //            Name = "Software",
        //        },
        //    };
        //    _productAttributeRepository.Insert(productAttributes);
        //}

        //protected virtual void InstallCategories()
        //{
        //    //pictures
        //    var pictureService = EngineContext.Current.Resolve<IPictureService>();
        //    var sampleImagesPath = _webHelper.MapPath("~/content/samples/");



        //    var categoryTemplateInGridAndLines = _categoryTemplateRepository
        //        .Table.FirstOrDefault(pt => pt.Name == "Products in Grid or Lines");
        //    if (categoryTemplateInGridAndLines == null)
        //        throw new Exception("Category template cannot be loaded");


        //    //categories
        //    var allCategories = new List<Category>();
        //    var categoryComputers = new Category
        //    {
        //        Name = "Computers",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_computers.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Computers")).Id,
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 1,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryComputers);
        //    _categoryRepository.Insert(categoryComputers);


        //    var categoryDesktops = new Category
        //    {
        //        Name = "Desktops",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        ParentCategoryId = categoryComputers.Id,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_desktops.jpg"), "image/pjpeg", pictureService.GetPictureSeName("Desktops")).Id,
        //        PriceRanges = "-1000;1000-1200;1200-;",
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 1,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryDesktops);
        //    _categoryRepository.Insert(categoryDesktops);


        //    var categoryNotebooks = new Category
        //    {
        //        Name = "Notebooks",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        ParentCategoryId = categoryComputers.Id,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_notebooks.jpg"), "image/pjpeg", pictureService.GetPictureSeName("Notebooks")).Id,
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 2,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryNotebooks);
        //    _categoryRepository.Insert(categoryNotebooks);


        //    var categorySoftware = new Category
        //    {
        //        Name = "Software",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        ParentCategoryId = categoryComputers.Id,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_software.jpg"), "image/pjpeg", pictureService.GetPictureSeName("Software")).Id,
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 3,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categorySoftware);
        //    _categoryRepository.Insert(categorySoftware);


        //    var categoryElectronics = new Category
        //    {
        //        Name = "Electronics",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_electronics.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Electronics")).Id,
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        ShowOnHomePage = true,
        //        DisplayOrder = 2,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryElectronics);
        //    _categoryRepository.Insert(categoryElectronics);


        //    var categoryCameraPhoto = new Category
        //    {
        //        Name = "Camera & photo",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        ParentCategoryId = categoryElectronics.Id,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_camera_photo.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Camera, photo")).Id,
        //        PriceRanges = "-500;500-;",
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 1,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryCameraPhoto);
        //    _categoryRepository.Insert(categoryCameraPhoto);


        //    var categoryCellPhones = new Category
        //    {
        //        Name = "Cell phones",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        ParentCategoryId = categoryElectronics.Id,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_cell_phones.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Cell phones")).Id,
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 2,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryCellPhones);
        //    _categoryRepository.Insert(categoryCellPhones);


        //    var categoryOthers = new Category
        //    {
        //        Name = "Others",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        ParentCategoryId = categoryElectronics.Id,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_accessories.jpg"), "image/pjpeg", pictureService.GetPictureSeName("Accessories")).Id,
        //        IncludeInTopMenu = true,
        //        PriceRanges = "-100;100-;",
        //        Published = true,
        //        DisplayOrder = 3,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryOthers);
        //    _categoryRepository.Insert(categoryOthers);


        //    var categoryApparel = new Category
        //    {
        //        Name = "Apparel",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_apparel.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Apparel")).Id,
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        ShowOnHomePage = true,
        //        DisplayOrder = 3,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryApparel);
        //    _categoryRepository.Insert(categoryApparel);


        //    var categoryShoes = new Category
        //    {
        //        Name = "Shoes",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        ParentCategoryId = categoryApparel.Id,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_shoes.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Shoes")).Id,
        //        PriceRanges = "-500;500-;",
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 1,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryShoes);
        //    _categoryRepository.Insert(categoryShoes);


        //    var categoryClothing = new Category
        //    {
        //        Name = "Clothing",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        ParentCategoryId = categoryApparel.Id,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_clothing.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Clothing")).Id,
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 2,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryClothing);
        //    _categoryRepository.Insert(categoryClothing);


        //    var categoryAccessories = new Category
        //    {
        //        Name = "Accessories",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        ParentCategoryId = categoryApparel.Id,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_apparel_accessories.jpg"), "image/pjpeg", pictureService.GetPictureSeName("Apparel Accessories")).Id,
        //        IncludeInTopMenu = true,
        //        PriceRanges = "-100;100-;",
        //        Published = true,
        //        DisplayOrder = 3,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryAccessories);
        //    _categoryRepository.Insert(categoryAccessories);


        //    var categoryDigitalDownloads = new Category
        //    {
        //        Name = "Digital downloads",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_digital_downloads.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Digital downloads")).Id,
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        ShowOnHomePage = true,
        //        DisplayOrder = 4,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryDigitalDownloads);
        //    _categoryRepository.Insert(categoryDigitalDownloads);


        //    var categoryBooks = new Category
        //    {
        //        Name = "Books",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        MetaKeywords = "Books, Dictionary, Textbooks",
        //        MetaDescription = "Books category description",
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_book.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Book")).Id,
        //        PriceRanges = "-25;25-50;50-;",
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 5,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryBooks);
        //    _categoryRepository.Insert(categoryBooks);


        //    var categoryJewelry = new Category
        //    {
        //        Name = "Jewelry",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_jewelry.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Jewelry")).Id,
        //        PriceRanges = "0-500;500-700;700-3000;",
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 6,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryJewelry);
        //    _categoryRepository.Insert(categoryJewelry);

        //    var categoryGiftCards = new Category
        //    {
        //        Name = "Gift Cards",
        //        CategoryTemplateId = categoryTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "category_gift_cards.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Gift Cards")).Id,
        //        IncludeInTopMenu = true,
        //        Published = true,
        //        DisplayOrder = 7,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allCategories.Add(categoryGiftCards);
        //    _categoryRepository.Insert(categoryGiftCards);



        //    //search engine names
        //    foreach (var category in allCategories)
        //    {
        //        _urlRecordRepository.Insert(new UrlRecord
        //        {
        //            EntityId = category.Id,
        //            EntityName = "Category",
        //            LanguageId = 0,
        //            IsActive = true,
        //            Slug = category.ValidateSeName("", category.Name, true)
        //        });
        //    }
        //}

        //protected virtual void InstallManufacturers()
        //{
        //    var pictureService = EngineContext.Current.Resolve<IPictureService>();
        //    var sampleImagesPath = _webHelper.MapPath("~/content/samples/");

        //    var manufacturerTemplateInGridAndLines =
        //        _manufacturerTemplateRepository.Table.FirstOrDefault(pt => pt.Name == "Products in Grid or Lines");
        //    if (manufacturerTemplateInGridAndLines == null)
        //        throw new Exception("Manufacturer template cannot be loaded");

        //    var allManufacturers = new List<Manufacturer>();
        //    var manufacturerAsus = new Manufacturer
        //    {
        //        Name = "Apple",
        //        ManufacturerTemplateId = manufacturerTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        Published = true,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "manufacturer_apple.jpg"), "image/pjpeg", pictureService.GetPictureSeName("Apple")).Id,
        //        DisplayOrder = 1,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    _manufacturerRepository.Insert(manufacturerAsus);
        //    allManufacturers.Add(manufacturerAsus);


        //    var manufacturerHp = new Manufacturer
        //    {
        //        Name = "HP",
        //        ManufacturerTemplateId = manufacturerTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        Published = true,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "manufacturer_hp.jpg"), "image/pjpeg", pictureService.GetPictureSeName("Hp")).Id,
        //        DisplayOrder = 5,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    _manufacturerRepository.Insert(manufacturerHp);
        //    allManufacturers.Add(manufacturerHp);


        //    var manufacturerNike = new Manufacturer
        //    {
        //        Name = "Nike",
        //        ManufacturerTemplateId = manufacturerTemplateInGridAndLines.Id,
        //        PageSize = 6,
        //        AllowCustomersToSelectPageSize = true,
        //        PageSizeOptions = "6, 3, 9",
        //        Published = true,
        //        PictureId = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "manufacturer_nike.jpg"), "image/pjpeg", pictureService.GetPictureSeName("Nike")).Id,
        //        DisplayOrder = 5,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    _manufacturerRepository.Insert(manufacturerNike);
        //    allManufacturers.Add(manufacturerNike);

        //    //search engine names
        //    foreach (var manufacturer in allManufacturers)
        //    {
        //        _urlRecordRepository.Insert(new UrlRecord
        //        {
        //            EntityId = manufacturer.Id,
        //            EntityName = "Manufacturer",
        //            LanguageId = 0,
        //            IsActive = true,
        //            Slug = manufacturer.ValidateSeName("", manufacturer.Name, true)
        //        });
        //    }
        //}

        //protected virtual void InstallProducts(string defaultUserEmail)
        //{
        //    var productTemplateSimple = _productTemplateRepository.Table.FirstOrDefault(pt => pt.Name == "Simple product");
        //    if (productTemplateSimple == null)
        //        throw new Exception("Simple product template could not be loaded");
        //    var productTemplateGrouped = _productTemplateRepository.Table.FirstOrDefault(pt => pt.Name == "Grouped product");
        //    if (productTemplateGrouped == null)
        //        throw new Exception("Simple product template could not be loaded");

        //    //delivery date
        //    var deliveryDate = _deliveryDateRepository.Table.FirstOrDefault();
        //    if (deliveryDate == null)
        //        throw new Exception("No default deliveryDate could be loaded");

        //    //default customer/user
        //    var defaultCustomer = _customerRepository.Table.FirstOrDefault(x => x.Email == defaultUserEmail);
        //    if (defaultCustomer == null)
        //        throw new Exception("Cannot load default customer");

        //    //pictures
        //    var pictureService = EngineContext.Current.Resolve<IPictureService>();
        //    var sampleImagesPath = _webHelper.MapPath("~/content/samples/");

        //    //downloads
        //    var downloadService = EngineContext.Current.Resolve<IDownloadService>();
        //    var sampleDownloadsPath = _webHelper.MapPath("~/content/samples/");

        //    //products
        //    var allProducts = new List<Product>();

        //    #region Desktops


        //    var productBuildComputer = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Build your own computer",
        //        ShortDescription = "Build it",
        //        FullDescription = "<p>Fight back against cluttered workspaces with the stylish IBM zBC12 All-in-One desktop PC, featuring powerful computing resources and a stunning 20.1-inch widescreen display with stunning XBRITE-HiColor LCD technology. The black IBM zBC12 has a built-in microphone and MOTION EYE camera with face-tracking technology that allows for easy communication with friends and family. And it has a built-in DVD burner and Sony's Movie Store software so you can create a digital entertainment library for personal viewing at your convenience. Easy to setup and even easier to use, this JS-series All-in-One includes an elegantly designed keyboard and a USB mouse.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "build-your-own-computer",
        //        AllowCustomerReviews = true,
        //        Price = 1200M,
        //        IsShipEnabled = true,
        //        IsFreeShipping = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        ShowOnHomePage = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductAttributeMappings =
        //        {
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "Processor"),
        //                AttributeControlType = AttributeControlType.DropdownList,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "2.2 GHz Intel Pentium Dual-Core E2200",
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "2.5 GHz Intel Pentium Dual-Core E2200",
        //                        IsPreSelected = true,
        //                        PriceAdjustment = 15,
        //                        DisplayOrder = 2,
        //                    }
        //                }
        //            },
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "RAM"),
        //                AttributeControlType = AttributeControlType.DropdownList,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "2 GB",
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "4GB",
        //                        PriceAdjustment = 20,
        //                        DisplayOrder = 2,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "8GB",
        //                        PriceAdjustment = 60,
        //                        DisplayOrder = 3,
        //                    }
        //                }
        //            },
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "HDD"),
        //                AttributeControlType = AttributeControlType.RadioList,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "320 GB",
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "400 GB",
        //                        PriceAdjustment = 100,
        //                        DisplayOrder = 2,
        //                    }
        //                }
        //            },
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "OS"),
        //                AttributeControlType = AttributeControlType.RadioList,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Vista Home",
        //                        PriceAdjustment = 50,
        //                        IsPreSelected = true,
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Vista Premium",
        //                        PriceAdjustment = 60,
        //                        DisplayOrder = 2,
        //                    }
        //                }
        //            },
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "Software"),
        //                AttributeControlType = AttributeControlType.Checkboxes,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Microsoft Office",
        //                        PriceAdjustment = 50,
        //                        IsPreSelected = true,
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Acrobat Reader",
        //                        PriceAdjustment = 10,
        //                        DisplayOrder = 2,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Total Commander",
        //                        PriceAdjustment = 5,
        //                        DisplayOrder = 2,
        //                    }
        //                }
        //            }
        //        },
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Desktops"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productBuildComputer);
        //    productBuildComputer.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_Desktops_1.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productBuildComputer.Name)),
        //        DisplayOrder = 1,
        //    });
        //    productBuildComputer.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_Desktops_2.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productBuildComputer.Name)),
        //        DisplayOrder = 2,
        //    });
        //    _productRepository.Insert(productBuildComputer);





        //    var productDigitalStorm = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Digital Storm VANQUISH 3 Custom Performance PC",
        //        ShortDescription = "Digital Storm Vanquish 3 Desktop PC",
        //        FullDescription = "<p>Blow the doors off today抯 most demanding games with maximum detail, speed, and power for an immersive gaming experience without breaking the bank.</p><p>Stay ahead of the competition, VANQUISH 3 is fully equipped to easily handle future upgrades, keeping your system on the cutting edge for years to come.</p><p>Each system is put through an extensive stress test, ensuring you experience zero bottlenecks and get the maximum performance from your hardware.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "compaq-presario-sr1519x-pentium-4-desktop-pc-with-cdrw",
        //        AllowCustomerReviews = true,
        //        Price = 1259M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Desktops"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productDigitalStorm);
        //    productDigitalStorm.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_DigitalStorm.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productDigitalStorm.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productDigitalStorm);





        //    var productLenovoIdeaCentre = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Lenovo IdeaCentre 600 All-in-One PC",
        //        ShortDescription = "",
        //        FullDescription = "<p>The A600 features a 21.5in screen, DVD or optional Blu-Ray drive, support for the full beans 1920 x 1080 HD, Dolby Home Cinema certification and an optional hybrid analogue/digital TV tuner.</p><p>Connectivity is handled by 802.11a/b/g - 802.11n is optional - and an ethernet port. You also get four USB ports, a Firewire slot, a six-in-one card reader and a 1.3- or two-megapixel webcam.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "hp-iq506-touchsmart-desktop-pc",
        //        AllowCustomerReviews = true,
        //        Price = 500M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Desktops"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productLenovoIdeaCentre);
        //    productLenovoIdeaCentre.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_LenovoIdeaCentre.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productLenovoIdeaCentre.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productLenovoIdeaCentre);




        //    #endregion

        //    #region Notebooks

        //    var productAppleMacBookPro = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Apple MacBook Pro 13-inch",
        //        ShortDescription = "A groundbreaking Retina display. A new force-sensing trackpad. All-flash architecture. Powerful dual-core and quad-core Intel processors. Together, these features take the notebook to a new level of performance. And they will do the same for you in everything you create.",
        //        FullDescription = "<p>With fifth-generation Intel Core processors, the latest graphics, and faster flash storage, the incredibly advanced MacBook Pro with Retina display moves even further ahead in performance and battery life.* *Compared with the previous generation.</p><p>Retina display with 2560-by-1600 resolution</p><p>Fifth-generation dual-core Intel Core i5 processor</p><p>Intel Iris Graphics</p><p>Up to 9 hours of battery life1</p><p>Faster flash storage2</p><p>802.11ac Wi-Fi</p><p>Two Thunderbolt 2 ports for connecting high-performance devices and transferring data at lightning speed</p><p>Two USB 3 ports (compatible with USB 2 devices) and HDMI</p><p>FaceTime HD camera</p><p>Pages, Numbers, Keynote, iPhoto, iMovie, GarageBand included</p><p>OS X, the world's most advanced desktop operating system</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "asus-eee-pc-1000ha-10-inch-netbook",
        //        AllowCustomerReviews = true,
        //        Price = 1800M,
        //        IsShipEnabled = true,
        //        IsFreeShipping = true,
        //        Weight = 3,
        //        Length = 3,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        ShowOnHomePage = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Notebooks"),
        //                DisplayOrder = 1,
        //            }
        //        },
        //        ProductManufacturers =
        //        {
        //            new ProductManufacturer
        //            {
        //                Manufacturer = _manufacturerRepository.Table.Single(c => c.Name == "Apple"),
        //                DisplayOrder = 2,
        //            }
        //        },
        //        ProductSpecificationAttributes =
        //        {
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 1,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Screensize").SpecificationAttributeOptions.Single(sao => sao.Name == "13.0''")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 2,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "CPU Type").SpecificationAttributeOptions.Single(sao => sao.Name == "Intel Core i5")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 3,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Memory").SpecificationAttributeOptions.Single(sao => sao.Name == "4 GB")
        //            }
        //            //new ProductSpecificationAttribute
        //            //{
        //            //    AllowFiltering = false,
        //            //    ShowOnProductPage = true,
        //            //    DisplayOrder = 4,
        //            //    SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Hardrive").SpecificationAttributeOptions.Single(sao => sao.Name == "160 GB")
        //            //}
        //        }
        //    };
        //    allProducts.Add(productAppleMacBookPro);
        //    productAppleMacBookPro.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_macbook_1.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productAppleMacBookPro.Name)),
        //        DisplayOrder = 1,
        //    });
        //    productAppleMacBookPro.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_macbook_2.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productAppleMacBookPro.Name)),
        //        DisplayOrder = 2,
        //    });
        //    _productRepository.Insert(productAppleMacBookPro);





        //    var productAsusN551JK = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Asus N551JK-XO076H Laptop",
        //        ShortDescription = "Laptop Asus N551JK Intel Core i7-4710HQ 2.5 GHz, RAM 16GB, HDD 1TB, Video NVidia GTX 850M 4GB, BluRay, 15.6, Full HD, Win 8.1",
        //        FullDescription = "<p>The ASUS N550JX combines cutting-edge audio and visual technology to deliver an unsurpassed multimedia experience. A full HD wide-view IPS panel is tailor-made for watching movies and the intuitive touchscreen makes for easy, seamless navigation. ASUS has paired the N550JX抯 impressive display with SonicMaster Premium, co-developed with Bang & Olufsen ICEpower?audio experts, for true surround sound. A quad-speaker array and external subwoofer combine for distinct vocals and a low bass that you can feel.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "asus-eee-pc-900ha-89-inch-netbook-black",
        //        AllowCustomerReviews = true,
        //        Price = 1500M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Notebooks"),
        //                DisplayOrder = 1,
        //            }
        //        },
        //        ProductSpecificationAttributes =
        //        {
        //             new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 1,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Screensize").SpecificationAttributeOptions.Single(sao => sao.Name == "15.6''")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 2,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "CPU Type").SpecificationAttributeOptions.Single(sao => sao.Name == "Intel Core i7")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 3,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Memory").SpecificationAttributeOptions.Single(sao => sao.Name == "16 GB")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 4,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Hardrive").SpecificationAttributeOptions.Single(sao => sao.Name == "1 TB")
        //            }
        //        }
        //    };
        //    allProducts.Add(productAsusN551JK);
        //    productAsusN551JK.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_asuspc_N551JK.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productAsusN551JK.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productAsusN551JK);





        //    var productSamsungSeries = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Samsung Series 9 NP900X4C Premium Ultrabook",
        //        ShortDescription = "Samsung Series 9 NP900X4C-A06US 15-Inch Ultrabook (1.70 GHz Intel Core i5-3317U Processor, 8GB DDR3, 128GB SSD, Windows 8) Ash Black",
        //        FullDescription = "<p>Designed with mobility in mind, Samsung's durable, ultra premium, lightweight Series 9 laptop (model NP900X4C-A01US) offers mobile professionals and power users a sophisticated laptop equally suited for work and entertainment. Featuring a minimalist look that is both simple and sophisticated, its polished aluminum uni-body design offers an iconic look and feel that pushes the envelope with an edge just 0.58 inches thin. This Series 9 laptop also includes a brilliant 15-inch SuperBright Plus display with HD+ technology, 128 GB Solid State Drive (SSD), 8 GB of system memory, and up to 10 hours of battery life.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "hp-pavilion-artist-edition-dv2890nr-141-inch-laptop",
        //        AllowCustomerReviews = true,
        //        Price = 1590M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        //ShowOnHomePage = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Notebooks"),
        //                DisplayOrder = 1,
        //            }
        //        },
        //        ProductSpecificationAttributes =
        //        {
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 1,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Screensize").SpecificationAttributeOptions.Single(sao => sao.Name == "15.0''")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 2,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "CPU Type").SpecificationAttributeOptions.Single(sao => sao.Name == "Intel Core i5")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 3,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Memory").SpecificationAttributeOptions.Single(sao => sao.Name == "8 GB")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 4,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Hardrive").SpecificationAttributeOptions.Single(sao => sao.Name == "128 GB")
        //            }
        //        }
        //    };
        //    allProducts.Add(productSamsungSeries);
        //    productSamsungSeries.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_SamsungNP900X4C.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productSamsungSeries.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productSamsungSeries);





        //    var productHpSpectre = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "HP Spectre XT Pro UltraBook",
        //        ShortDescription = "HP Spectre XT Pro UltraBook / Intel Core i5-2467M / 13.3 / 4GB / 128GB / Windows 7 Professional / Laptop",
        //        FullDescription = "<p>Introducing HP ENVY Spectre XT, the Ultrabook designed for those who want style without sacrificing substance. It's sleek. It's thin. And with Intel. Corer i5 processor and premium materials, it's designed to go anywhere from the bistro to the boardroom, it's unlike anything you've ever seen from HP.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "hp-pavilion-elite-m9150f-desktop-pc",
        //        AllowCustomerReviews = true,
        //        Price = 1350M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Notebooks"),
        //                DisplayOrder = 1,
        //            }
        //        },
        //        ProductManufacturers =
        //        {
        //            new ProductManufacturer
        //            {
        //                Manufacturer = _manufacturerRepository.Table.Single(c => c.Name == "HP"),
        //                DisplayOrder = 3,
        //            }
        //        },
        //        ProductSpecificationAttributes =
        //        {
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 1,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Screensize").SpecificationAttributeOptions.Single(sao => sao.Name == "13.3''")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 2,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "CPU Type").SpecificationAttributeOptions.Single(sao => sao.Name == "Intel Core i5")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 3,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Memory").SpecificationAttributeOptions.Single(sao => sao.Name == "4 GB")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 4,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Hardrive").SpecificationAttributeOptions.Single(sao => sao.Name == "128 GB")
        //            }
        //        }
        //    };
        //    allProducts.Add(productHpSpectre);
        //    productHpSpectre.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_HPSpectreXT_1.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productHpSpectre.Name)),
        //        DisplayOrder = 1,
        //    });
        //    productHpSpectre.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_HPSpectreXT_2.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productHpSpectre.Name)),
        //        DisplayOrder = 2,
        //    });
        //    _productRepository.Insert(productHpSpectre);



        //    var productHpEnvy = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "HP Envy 6-1180ca 15.6-Inch Sleekbook",
        //        ShortDescription = "HP ENVY 6-1202ea Ultrabook Beats Audio, 3rd generation Intel?CoreTM i7-3517U processor, 8GB RAM, 500GB HDD, Microsoft Windows 8, AMD Radeon HD 8750M (2 GB DDR3 dedicated)",
        //        FullDescription = "The UltrabookTM that's up for anything. Thin and light, the HP ENVY is the large screen UltrabookTM with Beats AudioTM. With a soft-touch base that makes it easy to grab and go, it's a laptop that's up for anything.<br><br><b>Features</b><br><br>- Windows 8 or other operating systems available<br><br><b>Top performance. Stylish design. Take notice.</b><br><br>- At just 19.8 mm (0.78 in) thin, the HP ENVY UltrabookTM is slim and light enough to take anywhere. It's the laptop that gets you noticed with the power to get it done.<br>- With an eye-catching metal design, it's a laptop that you want to carry with you. The soft-touch, slip-resistant base gives you the confidence to carry it with ease.<br><br><b>More entertaining. More gaming. More fun.</b><br><br>- Own the UltrabookTM with Beats AudioTM, dual speakers, a subwoofer, and an awesome display. Your music, movies and photo slideshows will always look and sound their best.<br>- Tons of video memory let you experience incredible gaming and multimedia without slowing down. Create and edit videos in a flash. And enjoy more of what you love to the fullest.<br>- The HP ENVY UltrabookTM is loaded with the ports you'd expect on a world-class laptop, but on a Sleekbook instead. Like HDMI, USB, RJ-45, and a headphone jack. You get all the right connections without compromising size.<br><br><b>Only from HP.</b><br><br>- Life heats up. That's why there's HP CoolSense technology, which automatically adjusts your notebook's temperature based on usage and conditions. It stays cool. You stay comfortable.<br>- With HP ProtectSmart, your notebook's data stays safe from accidental bumps and bruises. It senses motion and plans ahead, stopping your hard drive and protecting your entire digital life.<br>- Keep playing even in dimly lit rooms or on red eye flights. The optional backlit keyboard[1] is full-size so you don't compromise comfort. Backlit keyboard. Another bright idea.<br><br><b>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "hp-pavilion-g60-230us-160-inch-laptop",
        //        AllowCustomerReviews = true,
        //        Price = 1460M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Notebooks"),
        //                DisplayOrder = 1,
        //            }
        //        },
        //        ProductManufacturers =
        //        {
        //            new ProductManufacturer
        //            {
        //                Manufacturer = _manufacturerRepository.Table.Single(c => c.Name == "HP"),
        //                DisplayOrder = 4,
        //            }
        //        },
        //        ProductSpecificationAttributes =
        //        {
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 1,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Screensize").SpecificationAttributeOptions.Single(sao => sao.Name == "15.6''")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 2,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "CPU Type").SpecificationAttributeOptions.Single(sao => sao.Name == "Intel Core i7")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 3,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Memory").SpecificationAttributeOptions.Single(sao => sao.Name == "8 GB")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 4,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Hardrive").SpecificationAttributeOptions.Single(sao => sao.Name == "500 GB")
        //            }
        //        }
        //    };
        //    allProducts.Add(productHpEnvy);
        //    productHpEnvy.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_HpEnvy6.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productHpEnvy.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productHpEnvy);





        //    var productLenovoThinkpad = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Lenovo Thinkpad X1 Carbon Laptop",
        //        ShortDescription = "Lenovo Thinkpad X1 Carbon Touch Intel Core i7 14 Ultrabook",
        //        FullDescription = "<p>The X1 Carbon brings a new level of quality to the ThinkPad legacy of high standards and innovation. It starts with the durable, carbon fiber-reinforced roll cage, making for the best Ultrabook construction available, and adds a host of other new features on top of the old favorites. Because for 20 years, we haven't stopped innovating. And you shouldn't stop benefiting from that.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "toshiba-satellite-a305-s6908-154-inch-laptop",
        //        AllowCustomerReviews = true,
        //        Price = 1360M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Notebooks"),
        //                DisplayOrder = 1,
        //            }
        //        },
        //        ProductSpecificationAttributes =
        //        {
        //           new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = false,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 1,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Screensize").SpecificationAttributeOptions.Single(sao => sao.Name == "14.0''")
        //            },
        //            new ProductSpecificationAttribute
        //            {
        //                AllowFiltering = true,
        //                ShowOnProductPage = true,
        //                DisplayOrder = 2,
        //                SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "CPU Type").SpecificationAttributeOptions.Single(sao => sao.Name == "Intel Core i7")
        //            }
        //            //new ProductSpecificationAttribute
        //            //{
        //            //    AllowFiltering = true,
        //            //    ShowOnProductPage = true,
        //            //    DisplayOrder = 3,
        //            //    SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Memory").SpecificationAttributeOptions.Single(sao => sao.Name == "1 GB")
        //            //},
        //            //new ProductSpecificationAttribute
        //            //{
        //            //    AllowFiltering = false,
        //            //    ShowOnProductPage = true,
        //            //    DisplayOrder = 4,
        //            //    SpecificationAttributeOption = _specificationAttributeRepository.Table.Single(sa => sa.Name == "Hardrive").SpecificationAttributeOptions.Single(sao => sao.Name == "250 GB")
        //            //}
        //        }
        //    };
        //    allProducts.Add(productLenovoThinkpad);
        //    productLenovoThinkpad.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_LenovoThinkpad.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productLenovoThinkpad.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productLenovoThinkpad);

        //    #endregion

        //    #region Software


        //    var productAdobePhotoshop = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Adobe Photoshop CS4",
        //        ShortDescription = "Easily find and view all your photos",
        //        FullDescription = "<p>Adobe Photoshop CS4 software combines power and simplicity so you can make ordinary photos extraordinary; tell engaging stories in beautiful, personalized creations for print and web; and easily find and view all your photos. New Photoshop.com membership* works with Photoshop CS4 so you can protect your photos with automatic online backup and 2 GB of storage; view your photos anywhere you are; and share your photos in fun, interactive ways with invitation-only Online Albums.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "adobe-photoshop-elements-7",
        //        AllowCustomerReviews = true,
        //        Price = 75M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 3,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Software"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productAdobePhotoshop);
        //    productAdobePhotoshop.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_AdobePhotoshop.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productAdobePhotoshop.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productAdobePhotoshop);






        //    var productWindows8Pro = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Windows 8 Pro",
        //        ShortDescription = "Windows 8 is a Microsoft operating system that was released in 2012 as part of the company's Windows NT OS family. ",
        //        FullDescription = "<p>Windows 8 Pro is comparable to Windows 7 Professional and Ultimate and is targeted towards enthusiasts and business users; it includes all the features of Windows 8. Additional features include the ability to receive Remote Desktop connections, the ability to participate in a Windows Server domain, Encrypting File System, Hyper-V, and Virtual Hard Disk Booting, Group Policy as well as BitLocker and BitLocker To Go. Windows Media Center functionality is available only for Windows 8 Pro as a separate software package.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "corel-paint-shop-pro-photo-x2",
        //        AllowCustomerReviews = true,
        //        Price = 65M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 3,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Software"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productWindows8Pro);
        //    productWindows8Pro.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_Windows8.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productWindows8Pro.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productWindows8Pro);





        //    var productSoundForge = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Sound Forge Pro 11",
        //        ShortDescription = "Advanced audio waveform editor.",
        //        FullDescription = "<p>Sound Forge?Pro is the application of choice for a generation of creative and prolific artists, producers, and editors. Record audio quickly on a rock-solid platform, address sophisticated audio processing tasks with surgical precision, and render top-notch master files with ease. New features include one-touch recording, metering for the new critical standards, more repair and restoration tools, and exclusive round-trip interoperability with SpectraLayers Pro. Taken together, these enhancements make this edition of Sound Forge Pro the deepest and most advanced audio editing platform available.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "major-league-baseball-2k9",
        //        AllowCustomerReviews = true,
        //        Price = 54.99M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Software"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productSoundForge);
        //    productSoundForge.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_SoundForge.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productSoundForge.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productSoundForge);




        //    #endregion

        //    #region Camera, Photo


        //    //this one is a grouped product with two associated ones
        //    var productNikonD5500DSLR = new Product
        //    {
        //        ProductType = ProductType.GroupedProduct,
        //        VisibleIndividually = true,
        //        Name = "Nikon D5500 DSLR",
        //        ShortDescription = "Slim, lightweight Nikon D5500 packs a vari-angle touchscreen",
        //        FullDescription = "Nikon has announced its latest DSLR, the D5500. A lightweight, compact DX-format camera with a 24.2MP sensor, it抯 the first of its type to offer a vari-angle touchscreen. The D5500 replaces the D5300 in Nikon抯 range, and while it offers much the same features the company says it抯 a much slimmer and lighter prospect. There抯 a deep grip for easier handling and built-in Wi-Fi that lets you transfer and share shots via your phone or tablet.",
        //        ProductTemplateId = productTemplateGrouped.Id,
        //        //SeName = "canon-digital-slr-camera",
        //        AllowCustomerReviews = true,
        //        Published = true,
        //        Price = 670M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Camera & photo"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productNikonD5500DSLR);
        //    productNikonD5500DSLR.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_NikonCamera_1.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productNikonD5500DSLR.Name)),
        //        DisplayOrder = 1,
        //    });
        //    productNikonD5500DSLR.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_NikonCamera_2.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productNikonD5500DSLR.Name)),
        //        DisplayOrder = 2,
        //    });
        //    _productRepository.Insert(productNikonD5500DSLR);
        //    var productNikonD5500DSLR_associated_1 = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = false, //hide this products
        //        ParentGroupedProductId = productNikonD5500DSLR.Id,
        //        Name = "Nikon D5500 DSLR - Black",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "canon-digital-slr-camera-black",
        //        AllowCustomerReviews = true,
        //        Published = true,
        //        Price = 670M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allProducts.Add(productNikonD5500DSLR_associated_1);
        //    productNikonD5500DSLR_associated_1.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_NikonCamera_black.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Canon Digital SLR Camera - Black")),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productNikonD5500DSLR_associated_1);
        //    var productNikonD5500DSLR_associated_2 = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = false, //hide this products
        //        ParentGroupedProductId = productNikonD5500DSLR.Id,
        //        Name = "Nikon D5500 DSLR - Red",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "canon-digital-slr-camera-silver",
        //        AllowCustomerReviews = true,
        //        Published = true,
        //        Price = 630M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow
        //    };
        //    allProducts.Add(productNikonD5500DSLR_associated_2);
        //    productNikonD5500DSLR_associated_2.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_NikonCamera_red.jpeg"), "image/jpeg", pictureService.GetPictureSeName("Canon Digital SLR Camera - Silver")),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productNikonD5500DSLR_associated_2);





        //    var productLeica = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Leica T Mirrorless Digital Camera",
        //        ShortDescription = "Leica T (Typ 701) Silver",
        //        FullDescription = "<p>The new Leica T offers a minimalist design that's crafted from a single block of aluminum.  Made in Germany and assembled by hand, this 16.3 effective mega pixel camera is easy to use.  With a massive 3.7 TFT LCD intuitive touch screen control, the user is able to configure and save their own menu system.  The Leica T has outstanding image quality and also has 16GB of built in memory.  This is Leica's first system camera to use Wi-Fi.  Add the T-App to your portable iOS device and be able to transfer and share your images (free download from the Apple App Store)</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "canon-vixia-hf100-camcorder",
        //        AllowCustomerReviews = true,
        //        Price = 530M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Camera & photo"),
        //                DisplayOrder = 3,
        //            }
        //        }
        //    };
        //    allProducts.Add(productLeica);
        //    productLeica.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_LeicaT.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productLeica.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productLeica);






        //    var productAppleICam = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Apple iCam",
        //        ShortDescription = "Photography becomes smart",
        //        FullDescription = "<p>A few months ago we featured the amazing WVIL camera, by many considered the future of digital photography. This is another very good looking concept, iCam is the vision of Italian designer Antonio DeRosa, the idea is to have a device that attaches to the iPhone 5, which then allows the user to have a camera with interchangeable lenses. The device would also feature a front-touch screen and a projector. Would be great if apple picked up on this and made it reality.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "panasonic-hdc-sdt750k-high-definition-3d-camcorder",
        //        AllowCustomerReviews = true,
        //        Price = 1300M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Camera & photo"),
        //                DisplayOrder = 2,
        //            }
        //        },
        //        ProductManufacturers =
        //        {
        //            new ProductManufacturer
        //            {
        //                Manufacturer = _manufacturerRepository.Table.Single(c => c.Name == "Apple"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productAppleICam);
        //    productAppleICam.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_iCam.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productAppleICam.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productAppleICam);




        //    #endregion

        //    #region Cell Phone

        //    var productHtcOne = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "HTC One M8 Android L 5.0 Lollipop",
        //        ShortDescription = "HTC - One (M8) 4G LTE Cell Phone with 32GB Memory - Gunmetal (Sprint)",
        //        FullDescription = "<p><b>HTC One (M8) Cell Phone for Sprint:</b> With its brushed-metal design and wrap-around unibody frame, the HTC One (M8) is designed to fit beautifully in your hand. It's fun to use with amped up sound and a large Full HD touch screen, and intuitive gesture controls make it seem like your phone almost knows what you need before you do. <br><br>Sprint Easy Pay option available in store.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "blackberry-bold-9000-phone-black-att",
        //        AllowCustomerReviews = true,
        //        Price = 245M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        ShowOnHomePage = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Cell phones"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productHtcOne);
        //    productHtcOne.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_HTC_One_M8.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productHtcOne.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productHtcOne);






        //    var productHtcOneMini = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "HTC One Mini Blue",
        //        ShortDescription = "HTC One and HTC One Mini now available in bright blue hue",
        //        FullDescription = "<p>HTC One mini smartphone with 4.30-inch 720x1280 display powered by 1.4GHz processor alongside 1GB RAM and 4-Ultrapixel rear camera.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "samsung-rugby-a837-phone-black-att",
        //        AllowCustomerReviews = true,
        //        Price = 100M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Cell phones"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productHtcOneMini);
        //    productHtcOneMini.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_HTC_One_Mini_1.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productHtcOneMini.Name)),
        //        DisplayOrder = 1,
        //    });
        //    productHtcOneMini.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_HTC_One_Mini_2.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productHtcOneMini.Name)),
        //        DisplayOrder = 2,
        //    });
        //    _productRepository.Insert(productHtcOneMini);






        //    var productNokiaLumia = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Nokia Lumia 1020",
        //        ShortDescription = "Nokia Lumia 1020 4G Cell Phone (Unlocked)",
        //        FullDescription = "<p>Capture special moments for friends and family with this Nokia Lumia 1020 32GB WHITE cell phone that features an easy-to-use 41.0MP rear-facing camera and a 1.2MP front-facing camera. The AMOLED touch screen offers 768 x 1280 resolution for crisp visuals.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "sony-dcr-sr85-1mp-60gb-hard-drive-handycam-camcorder",
        //        AllowCustomerReviews = true,
        //        Price = 349M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Cell phones"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productNokiaLumia);
        //    productNokiaLumia.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_Lumia1020.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productNokiaLumia.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productNokiaLumia);


        //    #endregion

        //    #region Others



        //    var productBeatsPill = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Beats Pill 2.0 Wireless Speaker",
        //        ShortDescription = "<b>Pill 2.0 Portable Bluetooth Speaker (1-Piece):</b> Watch your favorite movies and listen to music with striking sound quality. This lightweight, portable speaker is easy to take with you as you travel to any destination, keeping you entertained wherever you are. ",
        //        FullDescription = "<p<ul><li>Pair and play with your Bluetooth?device with 30 foot range</li><li>Built-in speakerphone</li><li>7 hour rechargeable battery</li><li>Power your other devices with USB charge out</li><li>Tap two Beats Pills?together for twice the sound with Beats Bond?/li></ul></p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "acer-aspire-one-89-mini-notebook-case-black",
        //        AllowCustomerReviews = true,
        //        Price = 79.99M,
        //        IsShipEnabled = true,
        //        IsFreeShipping = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 3,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        TierPrices =
        //        {
        //            new TierPrice
        //            {
        //                Quantity = 2,
        //                Price = 19
        //            },
        //            new TierPrice
        //            {
        //                Quantity = 5,
        //                Price = 17
        //            },
        //            new TierPrice
        //            {
        //                Quantity = 10,
        //                Price = 15
        //            }
        //        },
        //        HasTierPrices = true,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Others"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productBeatsPill);
        //    productBeatsPill.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_PillBeats_1.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productBeatsPill.Name)),
        //        DisplayOrder = 1,
        //    });
        //    productBeatsPill.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_PillBeats_2.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productBeatsPill.Name)),
        //        DisplayOrder = 2,
        //    });
        //    _productRepository.Insert(productBeatsPill);





        //    var productUniversalTabletCover = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Universal 7-8 Inch Tablet Cover",
        //        ShortDescription = "Universal protection for 7-inch & 8-inch tablets",
        //        FullDescription = "<p>Made of durable polyurethane, our Universal Cover is slim, lightweight, and strong, with protective corners that stretch to hold most 7 and 8-inch tablets securely. This tough case helps protects your tablet from bumps, scuffs, and dings.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "apc-back-ups-rs-800va-ups-800-va-ups-battery-lead-acid-br800blk",
        //        AllowCustomerReviews = true,
        //        Price = 39M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 3,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Others"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productUniversalTabletCover);
        //    productUniversalTabletCover.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_TabletCover.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productUniversalTabletCover.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productUniversalTabletCover);




        //    var productPortableSoundSpeakers = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Portable Sound Speakers",
        //        ShortDescription = "Universall portable sound speakers",
        //        FullDescription = "<p>Your phone cut the cord, now it's time for you to set your music free and buy a Bluetooth speaker. Thankfully, there's one suited for everyone out there.</p><p>Some Bluetooth speakers excel at packing in as much functionality as the unit can handle while keeping the price down. Other speakers shuck excess functionality in favor of premium build materials instead. Whatever path you choose to go down, you'll be greeted with many options to suit your personal tastes.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "microsoft-bluetooth-notebook-mouse-5000-macwindows",
        //        AllowCustomerReviews = true,
        //        Price = 37M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Electronics & Software").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Others"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productPortableSoundSpeakers);
        //    productPortableSoundSpeakers.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_Speakers.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productPortableSoundSpeakers.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productPortableSoundSpeakers);


        //    #endregion

        //    #region Shoes


        //    var productNikeFloral = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Nike Floral Roshe Customized Running Shoes",
        //        ShortDescription = "When you ran across these shoes, you will immediately fell in love and needed a pair of these customized beauties.",
        //        FullDescription = "<p>Each Rosh Run is personalized and exclusive, handmade in our workshop Custom. Run Your Rosh creations born from the hand of an artist specialized in sneakers, more than 10 years of experience.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "adidas-womens-supernova-csh-7-running-shoe",
        //        AllowCustomerReviews = true,
        //        Price = 40M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductAttributeMappings =
        //        {
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "Size"),
        //                AttributeControlType = AttributeControlType.DropdownList,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "8",
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "9",
        //                        DisplayOrder = 2,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "10",
        //                        DisplayOrder = 3,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "11",
        //                        DisplayOrder = 4,
        //                    }
        //                }
        //            },
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "Color"),
        //                AttributeControlType = AttributeControlType.DropdownList,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "White/Blue",
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "White/Black",
        //                        DisplayOrder = 2,
        //                    },
        //                }
        //            }
        //        },
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Shoes"),
        //                DisplayOrder = 1,
        //            }
        //        },
        //        ProductManufacturers =
        //        {
        //            new ProductManufacturer
        //            {
        //                Manufacturer = _manufacturerRepository.Table.Single(c => c.Name == "Nike"),
        //                DisplayOrder = 2,
        //            }
        //        }
        //    };
        //    allProducts.Add(productNikeFloral);
        //    productNikeFloral.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_NikeFloralShoe_1.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productNikeFloral.Name)),
        //        DisplayOrder = 1,
        //    });
        //    productNikeFloral.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_NikeFloralShoe_2.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productNikeFloral.Name)),
        //        DisplayOrder = 2,
        //    });
        //    _productRepository.Insert(productNikeFloral);




        //    var productAdidas = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "adidas Consortium Campus 80s Running Shoes",
        //        ShortDescription = "adidas Consortium Campus 80s Primeknit Light Maroon/Running Shoes",
        //        FullDescription = "<p>One of three colorways of the adidas Consortium Campus 80s Primeknit set to drop alongside each other. This pair comes in light maroon and running white. Featuring a maroon-based primeknit upper with white accents. A limited release, look out for these at select adidas Consortium accounts worldwide.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "etnies-mens-digit-sneaker",
        //        AllowCustomerReviews = true,
        //        Price = 27.56M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        //ShowOnHomePage = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductAttributeMappings =
        //        {
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "Size"),
        //                AttributeControlType = AttributeControlType.DropdownList,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "8",
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "9",
        //                        DisplayOrder = 2,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "10",
        //                        DisplayOrder = 3,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "11",
        //                        DisplayOrder = 4,
        //                    }
        //                }
        //            },
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "Color"),
        //                AttributeControlType = AttributeControlType.ColorSquares,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Red",
        //                        IsPreSelected = true,
        //                        ColorSquaresRgb = "#663030",
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Blue",
        //                        ColorSquaresRgb = "#363656",
        //                        DisplayOrder = 2,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Silver",
        //                        ColorSquaresRgb = "#c5c5d5",
        //                        DisplayOrder = 3,
        //                    }
        //                }
        //            }
        //        },
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Shoes"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productAdidas);
        //    productAdidas.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_adidas.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productAdidas.Name)),
        //        DisplayOrder = 1,
        //    });
        //    productAdidas.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_adidas_2.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productAdidas.Name)),
        //        DisplayOrder = 2,
        //    });
        //    productAdidas.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_adidas_3.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productAdidas.Name)),
        //        DisplayOrder = 3,
        //    });


        //    _productRepository.Insert(productAdidas);

        //    productAdidas.ProductAttributeMappings.Where(x => x.ProductAttribute.Name == "Color").First().ProductAttributeValues.Where(x => x.Name == "Red").First().PictureId = productAdidas.ProductPictures.ElementAt(0).PictureId;
        //    productAdidas.ProductAttributeMappings.Where(x => x.ProductAttribute.Name == "Color").First().ProductAttributeValues.Where(x => x.Name == "Blue").First().PictureId = productAdidas.ProductPictures.ElementAt(1).PictureId;
        //    productAdidas.ProductAttributeMappings.Where(x => x.ProductAttribute.Name == "Color").First().ProductAttributeValues.Where(x => x.Name == "Silver").First().PictureId = productAdidas.ProductPictures.ElementAt(2).PictureId;
        //    _productRepository.Update(productAdidas);




        //    var productNikeZoom = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Nike SB Zoom Stefan Janoski \"Medium Mint\"",
        //        ShortDescription = "Nike SB Zoom Stefan Janoski Dark Grey Medium Mint Teal ...",
        //        FullDescription = "The newly Nike SB Zoom Stefan Janoski gets hit with a \"Medium Mint\" accents that sits atop a Dark Grey suede. Expected to drop in October.",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "v-blue-juniors-cuffed-denim-short-with-rhinestones",
        //        AllowCustomerReviews = true,
        //        Price = 30M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Shoes"),
        //                DisplayOrder = 1,
        //            }
        //        },
        //        ProductManufacturers =
        //        {
        //            new ProductManufacturer
        //            {
        //                Manufacturer = _manufacturerRepository.Table.Single(c => c.Name == "Nike"),
        //                DisplayOrder = 2,
        //            }
        //        }
        //    };
        //    allProducts.Add(productNikeZoom);
        //    productNikeZoom.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_NikeZoom.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productNikeZoom.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productNikeZoom);


        //    #endregion

        //    #region Clothing

        //    var productNikeTailwind = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Nike Tailwind Loose Short-Sleeve Running Shirt",
        //        ShortDescription = "",
        //        FullDescription = "<p>Boost your adrenaline with the Nike?Women's Tailwind Running Shirt. The lightweight, slouchy fit is great for layering, and moisture-wicking fabrics keep you feeling at your best. This tee has a notched hem for an enhanced range of motion, while flat seams with reinforcement tape lessen discomfort and irritation over longer distances. Put your keys and card in the side zip pocket and take off in your Nike?running t-shirt.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "50s-rockabilly-polka-dot-top-jr-plus-size",
        //        AllowCustomerReviews = true,
        //        Published = true,
        //        Price = 15M,
        //        IsShipEnabled = true,
        //        Weight = 1,
        //        Length = 2,
        //        Width = 3,
        //        Height = 3,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductAttributeMappings =
        //        {
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "Size"),
        //                AttributeControlType = AttributeControlType.DropdownList,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Small",
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "1X",
        //                        DisplayOrder = 2,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "2X",
        //                        DisplayOrder = 3,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "3X",
        //                        DisplayOrder = 4,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "4X",
        //                        DisplayOrder = 5,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "5X",
        //                        DisplayOrder = 6,
        //                    }
        //                }
        //            }
        //        },
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Clothing"),
        //                DisplayOrder = 1,
        //            }
        //        },
        //        ProductManufacturers =
        //        {
        //            new ProductManufacturer
        //            {
        //                Manufacturer = _manufacturerRepository.Table.Single(c => c.Name == "Nike"),
        //                DisplayOrder = 2,
        //            }
        //        }
        //    };
        //    allProducts.Add(productNikeTailwind);
        //    productNikeTailwind.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_NikeShirt.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productNikeTailwind.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productNikeTailwind);




        //    var productOversizedWomenTShirt = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Oversized Women T-Shirt",
        //        ShortDescription = "",
        //        FullDescription = "<p>This oversized women t-Shirt needs minimum ironing. It is a great product at a great value!</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "arrow-mens-wrinkle-free-pinpoint-solid-long-sleeve",
        //        AllowCustomerReviews = true,
        //        Price = 24M,
        //        IsShipEnabled = true,
        //        Weight = 4,
        //        Length = 3,
        //        Width = 3,
        //        Height = 3,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        TierPrices =
        //        {
        //            new TierPrice
        //            {
        //                Quantity = 3,
        //                Price = 21
        //            },
        //            new TierPrice
        //            {
        //                Quantity = 7,
        //                Price = 19
        //            },
        //            new TierPrice
        //            {
        //                Quantity = 10,
        //                Price = 16
        //            }
        //        },
        //        HasTierPrices = true,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Clothing"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productOversizedWomenTShirt);
        //    productOversizedWomenTShirt.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_WomenTShirt.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productOversizedWomenTShirt.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productOversizedWomenTShirt);




        //    var productCustomTShirt = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Custom T-Shirt",
        //        ShortDescription = "T-Shirt - Add Your Content",
        //        FullDescription = "<p>Comfort comes in all shapes and forms, yet this tee out does it all. Rising above the rest, our classic cotton crew provides the simple practicality you need to make it through the day. Tag-free, relaxed fit wears well under dress shirts or stands alone in laid-back style. Reinforced collar and lightweight feel give way to long-lasting shape and breathability. One less thing to worry about, rely on this tee to provide comfort and ease with every wear.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "custom-t-shirt",
        //        AllowCustomerReviews = true,
        //        Price = 15M,
        //        IsShipEnabled = true,
        //        Weight = 4,
        //        Length = 3,
        //        Width = 3,
        //        Height = 3,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductAttributeMappings =
        //        {
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "Custom Text"),
        //                TextPrompt = "Enter your text:",
        //                AttributeControlType = AttributeControlType.TextBox,
        //                IsRequired = true,
        //            }
        //        },
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Clothing"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productCustomTShirt);
        //    productCustomTShirt.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_CustomTShirt.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productCustomTShirt.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productCustomTShirt);






        //    var productLeviJeans = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Levi's 511 Jeans",
        //        ShortDescription = "Levi's Faded Black 511 Jeans ",
        //        FullDescription = "<p>Between a skinny and straight fit, our 511&trade; slim fit jeans are cut close without being too restricting. Slim throughout the thigh and leg opening for a long and lean look.</p><ul><li>Slouch1y at top; sits below the waist</li><li>Slim through the leg, close at the thigh and straight to the ankle</li><li>Stretch for added comfort</li><li>Classic five-pocket styling</li><li>99% Cotton, 1% Spandex, 11.2 oz. - Imported</li></ul>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "levis-skinny-511-jeans",
        //        AllowCustomerReviews = true,
        //        Price = 43.5M,
        //        OldPrice = 55M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        TierPrices =
        //        {
        //            new TierPrice
        //            {
        //                Quantity = 3,
        //                Price = 40
        //            },
        //            new TierPrice
        //            {
        //                Quantity = 6,
        //                Price = 38
        //            },
        //            new TierPrice
        //            {
        //                Quantity = 10,
        //                Price = 35
        //            }
        //        },
        //        HasTierPrices = true,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Clothing"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productLeviJeans);

        //    productLeviJeans.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_LeviJeans_1.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productLeviJeans.Name)),
        //        DisplayOrder = 1,
        //    });
        //    productLeviJeans.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_LeviJeans_2.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productLeviJeans.Name)),
        //        DisplayOrder = 2,
        //    });
        //    _productRepository.Insert(productLeviJeans);


        //    #endregion

        //    #region Accessories


        //    var productObeyHat = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Obey Propaganda Hat",
        //        ShortDescription = "",
        //        FullDescription = "<p>Printed poplin 5 panel camp hat with debossed leather patch and web closure</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "indiana-jones-shapeable-wool-hat",
        //        AllowCustomerReviews = true,
        //        Price = 30M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductAttributeMappings =
        //        {
        //            new ProductAttributeMapping
        //            {
        //                ProductAttribute = _productAttributeRepository.Table.Single(x => x.Name == "Size"),
        //                AttributeControlType = AttributeControlType.DropdownList,
        //                IsRequired = true,
        //                ProductAttributeValues =
        //                {
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Small",
        //                        DisplayOrder = 1,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Medium",
        //                        DisplayOrder = 2,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "Large",
        //                        DisplayOrder = 3,
        //                    },
        //                    new ProductAttributeValue
        //                    {
        //                        AttributeValueType = AttributeValueType.Simple,
        //                        Name = "X-Large",
        //                        DisplayOrder = 4,
        //                    }
        //                }
        //            }
        //        },
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Accessories"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productObeyHat);
        //    productObeyHat.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_hat.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productObeyHat.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productObeyHat);







        //    var productBelt = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Reversible Horseferry Check Belt",
        //        ShortDescription = "Reversible belt in Horseferry check with smooth leather trim",
        //        FullDescription = "<p>Reversible belt in Horseferry check with smooth leather trim</p><p>Leather lining, polished metal buckle</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "nike-golf-casual-belt",
        //        AllowCustomerReviews = true,
        //        Price = 45M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Accessories"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productBelt);
        //    productBelt.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_Belt.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productBelt.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productBelt);






        //    var productSunglasses = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Ray Ban Aviator Sunglasses",
        //        ShortDescription = "Aviator sunglasses are one of the first widely popularized styles of modern day sunwear.",
        //        FullDescription = "<p>Since 1937, Ray-Ban can genuinely claim the title as the world's leading sunglasses and optical eyewear brand. Combining the best of fashion and sports performance, the Ray-Ban line of Sunglasses delivers a truly classic style that will have you looking great today and for years to come.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "ray-ban-aviator-sunglasses-rb-3025",
        //        AllowCustomerReviews = true,
        //        Price = 25M,
        //        IsShipEnabled = true,
        //        Weight = 7,
        //        Length = 7,
        //        Width = 7,
        //        Height = 7,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Apparel").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Accessories"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productSunglasses);
        //    productSunglasses.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_Sunglasses.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productSunglasses.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productSunglasses);

        //    #endregion

        //    #region Digital Downloads


        //    var downloadNightVision1 = new Download
        //    {
        //        DownloadGuid = Guid.NewGuid(),
        //        ContentType = "application/x-zip-co",
        //        DownloadBinary = File.ReadAllBytes(sampleDownloadsPath + "product_NightVision_1.zip"),
        //        Extension = ".zip",
        //        Filename = "Night_Vision_1",
        //        IsNew = true,
        //    };
        //    downloadService.InsertDownload(downloadNightVision1);
        //    var downloadNightVision2 = new Download
        //    {
        //        DownloadGuid = Guid.NewGuid(),
        //        ContentType = "text/plain",
        //        DownloadBinary = File.ReadAllBytes(sampleDownloadsPath + "product_NightVision_2.txt"),
        //        Extension = ".txt",
        //        Filename = "Night_Vision_1",
        //        IsNew = true,
        //    };
        //    downloadService.InsertDownload(downloadNightVision2);
        //    var productNightVision = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Night Visions",
        //        ShortDescription = "Night Visions is the debut studio album by American rock band Imagine Dragons.",
        //        FullDescription = "<p>Original Release Date: September 4, 2012</p><p>Release Date: September 4, 2012</p><p>Genre - Alternative rock, indie rock, electronic rock</p><p>Label - Interscope/KIDinaKORNER</p><p>Copyright: (C) 2011 Interscope Records</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "poker-face",
        //        AllowCustomerReviews = true,
        //        Price = 2.8M,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Downloadable Products").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.DontManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        IsDownload = true,
        //        DownloadId = downloadNightVision1.Id,
        //        DownloadActivationType = DownloadActivationType.WhenOrderIsPaid,
        //        UnlimitedDownloads = true,
        //        HasUserAgreement = false,
        //        HasSampleDownload = true,
        //        SampleDownloadId = downloadNightVision2.Id,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Digital downloads"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productNightVision);
        //    productNightVision.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_NightVisions.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productNightVision.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productNightVision);





        //    var downloadIfYouWait1 = new Download
        //    {
        //        DownloadGuid = Guid.NewGuid(),
        //        ContentType = "application/x-zip-co",
        //        DownloadBinary = File.ReadAllBytes(sampleDownloadsPath + "product_IfYouWait_1.zip"),
        //        Extension = ".zip",
        //        Filename = "If_You_Wait_1",
        //        IsNew = true,
        //    };
        //    downloadService.InsertDownload(downloadIfYouWait1);
        //    var downloadIfYouWait2 = new Download
        //    {
        //        DownloadGuid = Guid.NewGuid(),
        //        ContentType = "text/plain",
        //        DownloadBinary = File.ReadAllBytes(sampleDownloadsPath + "product_IfYouWait_2.txt"),
        //        Extension = ".txt",
        //        Filename = "If_You_Wait_1",
        //        IsNew = true,
        //    };
        //    downloadService.InsertDownload(downloadIfYouWait2);
        //    var productIfYouWait = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "If You Wait",
        //        ShortDescription = "If You Wait is the debut studio album by English indie pop band London Grammar",
        //        FullDescription = "<p>Original Release Date: September 6, 2013</p><p>Genre - Electronica, dream pop downtempo, pop</p><p>Label - Metal & Dust/Ministry of Sound</p><p>Producer - Tim Bran, Roy Kerr London, Grammar</p><p>Length - 43:22</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "single-ladies-put-a-ring-on-it",
        //        AllowCustomerReviews = true,
        //        Price = 3M,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Downloadable Products").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.DontManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        IsDownload = true,
        //        DownloadId = downloadIfYouWait1.Id,
        //        DownloadActivationType = DownloadActivationType.WhenOrderIsPaid,
        //        UnlimitedDownloads = true,
        //        HasUserAgreement = false,
        //        HasSampleDownload = true,
        //        SampleDownloadId = downloadIfYouWait2.Id,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Digital downloads"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productIfYouWait);

        //    productIfYouWait.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_IfYouWait.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productIfYouWait.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productIfYouWait);





        //    var downloadScienceAndFaith = new Download
        //    {
        //        DownloadGuid = Guid.NewGuid(),
        //        ContentType = "application/x-zip-co",
        //        DownloadBinary = File.ReadAllBytes(sampleDownloadsPath + "product_ScienceAndFaith_1.zip"),
        //        Extension = ".zip",
        //        Filename = "Science_And_Faith",
        //        IsNew = true,
        //    };
        //    downloadService.InsertDownload(downloadScienceAndFaith);
        //    var productScienceAndFaith = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Science & Faith",
        //        ShortDescription = "Science & Faith is the second studio album by Irish pop rock band The Script.",
        //        FullDescription = "<p># Original Release Date: September 10, 2010<br /># Label: RCA, Epic/Phonogenic(America)<br /># Copyright: 2010 RCA Records.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "the-battle-of-los-angeles",
        //        AllowCustomerReviews = true,
        //        Price = 3M,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Downloadable Products").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.DontManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        IsDownload = true,
        //        DownloadId = downloadScienceAndFaith.Id,
        //        DownloadActivationType = DownloadActivationType.WhenOrderIsPaid,
        //        UnlimitedDownloads = true,
        //        HasUserAgreement = false,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Digital downloads"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productScienceAndFaith);
        //    productScienceAndFaith.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_ScienceAndFaith.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productScienceAndFaith.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productScienceAndFaith);



        //    #endregion

        //    #region Books

        //    var productFahrenheit = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Fahrenheit 451 by Ray Bradbury",
        //        ShortDescription = "Fahrenheit 451 is a dystopian novel by Ray Bradbury published in 1953. It is regarded as one of his best works.",
        //        FullDescription = "<p>The novel presents a future American society where books are outlawed and firemen burn any that are found. The title refers to the temperature that Bradbury understood to be the autoignition point of paper.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "best-grilling-recipes",
        //        AllowCustomerReviews = true,
        //        Price = 27M,
        //        OldPrice = 30M,
        //        IsShipEnabled = true,
        //        IsFreeShipping = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Books").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Books"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productFahrenheit);
        //    productFahrenheit.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_Fahrenheit451.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productFahrenheit.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productFahrenheit);



        //    var productFirstPrizePies = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "First Prize Pies",
        //        ShortDescription = "Allison Kave made pies as a hobby, until one day her boyfriend convinced her to enter a Brooklyn pie-making contest. She won. In fact, her pies were such a hit that she turned pro.",
        //        FullDescription = "<p>First Prize Pies, a boutique, made-to-order pie business that originated on New York's Lower East Side, has become synonymous with tempting and unusual confections. For the home baker who is passionate about seasonal ingredients and loves a creative approach to recipes, First Prize Pies serves up 52 weeks of seasonal and eclectic pastries in an interesting pie-a-week format. Clear instructions, technical tips and creative encouragement guide novice bakers as well as pie mavens. With its nostalgia-evoking photos of homemade pies fresh out of the oven, First Prize Pies will be as giftable as it is practical.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "eatingwell-in-season",
        //        AllowCustomerReviews = true,
        //        Price = 51M,
        //        OldPrice = 67M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Books").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Books"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productFirstPrizePies);
        //    productFirstPrizePies.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_FirstPrizePies.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productFirstPrizePies.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productFirstPrizePies);







        //    var productPrideAndPrejudice = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Pride and Prejudice",
        //        ShortDescription = "Pride and Prejudice is a novel of manners by Jane Austen, first published in 1813.",
        //        FullDescription = "<p>Set in England in the early 19th century, Pride and Prejudice tells the story of Mr and Mrs Bennet's five unmarried daughters after the rich and eligible Mr Bingley and his status-conscious friend, Mr Darcy, have moved into their neighbourhood. While Bingley takes an immediate liking to the eldest Bennet daughter, Jane, Darcy has difficulty adapting to local society and repeatedly clashes with the second-eldest Bennet daughter, Elizabeth.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "the-best-skillet-recipes",
        //        AllowCustomerReviews = true,
        //        Price = 24M,
        //        OldPrice = 35M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Books").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Books"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productPrideAndPrejudice);
        //    productPrideAndPrejudice.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_PrideAndPrejudice.jpeg"), "image/jpeg", pictureService.GetPictureSeName(productPrideAndPrejudice.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productPrideAndPrejudice);



        //    #endregion

        //    #region Jewelry



        //    var productElegantGemstoneNecklace = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Elegant Gemstone Necklace",
        //        ShortDescription = "Classic and elegant gemstone necklace now available in our store",
        //        FullDescription = "<p>For those who like jewelry, creating their ownelegant jewelry from gemstone beads provides an economical way to incorporate genuine gemstones into your jewelry wardrobe. Manufacturers create beads from all kinds of precious gemstones and semi-precious gemstones, which are available in bead shops, craft stores, and online marketplaces.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "diamond-pave-earrings",
        //        AllowCustomerReviews = true,
        //        Price = 569M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Jewelry").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Jewelry"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productElegantGemstoneNecklace);
        //    productElegantGemstoneNecklace.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_GemstoneNecklaces.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productElegantGemstoneNecklace.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productElegantGemstoneNecklace);





        //    var productFlowerGirlBracelet = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Flower Girl Bracelet",
        //        ShortDescription = "Personalised Flower Braceled",
        //        FullDescription = "<p>This is a great gift for your flower girl to wear on your wedding day. A delicate bracelet that is made with silver plated soldered cable chain, gives this bracelet a dainty look for young wrist. A Swarovski heart, shown in Rose, hangs off a silver plated flower. Hanging alongside the heart is a silver plated heart charm with Flower Girl engraved on both sides. This is a great style for the younger flower girl.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "diamond-tennis-bracelet",
        //        AllowCustomerReviews = true,
        //        Price = 360M,
        //        IsShipEnabled = true,
        //        IsFreeShipping = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Jewelry").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Jewelry"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productFlowerGirlBracelet);
        //    productFlowerGirlBracelet.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_FlowerBracelet.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productFlowerGirlBracelet.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productFlowerGirlBracelet);








        //    var productEngagementRing = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "Vintage Style Engagement Ring",
        //        ShortDescription = "1.24 Carat (ctw) in 14K White Gold (Certified)",
        //        FullDescription = "<p>Dazzle her with this gleaming 14 karat white gold vintage proposal. A ravishing collection of 11 decadent diamonds come together to invigorate a superbly ornate gold shank. Total diamond weight on this antique style engagement ring equals 1 1/4 carat (ctw). Item includes diamond certificate.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "vintage-style-three-stone-diamond-engagement-ring",
        //        AllowCustomerReviews = true,
        //        Price = 2100M,
        //        IsShipEnabled = true,
        //        Weight = 2,
        //        Length = 2,
        //        Width = 2,
        //        Height = 2,
        //        TaxCategoryId = _taxCategoryRepository.Table.Single(tc => tc.Name == "Jewelry").Id,
        //        ManageInventoryMethod = ManageInventoryMethod.ManageStock,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        DisplayStockAvailability = true,
        //        LowStockActivity = LowStockActivity.DisableBuyButton,
        //        BackorderMode = BackorderMode.NoBackorders,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Jewelry"),
        //                DisplayOrder = 1,
        //            }
        //        }
        //    };
        //    allProducts.Add(productEngagementRing);
        //    productEngagementRing.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_EngagementRing_1.jpg"), "image/pjpeg", pictureService.GetPictureSeName(productEngagementRing.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(productEngagementRing);



        //    #endregion

        //    #region Gift Cards


        //    var product25GiftCard = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "$25 Virtual Gift Card",
        //        ShortDescription = "$25 Gift Card. Gift Cards must be redeemed through our site Web site toward the purchase of eligible products.",
        //        FullDescription = "<p>Gift Cards must be redeemed through our site Web site toward the purchase of eligible products. Purchases are deducted from the GiftCard balance. Any unused balance will be placed in the recipient's GiftCard account when redeemed. If an order exceeds the amount of the GiftCard, the balance must be paid with a credit card or other available payment method.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "25-virtual-gift-card",
        //        AllowCustomerReviews = true,
        //        Price = 25M,
        //        IsGiftCard = true,
        //        GiftCardType = GiftCardType.Virtual,
        //        ManageInventoryMethod = ManageInventoryMethod.DontManageStock,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        Published = true,
        //        ShowOnHomePage = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Gift Cards"),
        //                DisplayOrder = 2,
        //            }
        //        }
        //    };
        //    allProducts.Add(product25GiftCard);
        //    product25GiftCard.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_25giftcart.jpeg"), "image/jpeg", pictureService.GetPictureSeName(product25GiftCard.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(product25GiftCard);





        //    var product50GiftCard = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "$50 Physical Gift Card",
        //        ShortDescription = "$50 Gift Card. Gift Cards must be redeemed through our site Web site toward the purchase of eligible products.",
        //        FullDescription = "<p>Gift Cards must be redeemed through our site Web site toward the purchase of eligible products. Purchases are deducted from the GiftCard balance. Any unused balance will be placed in the recipient's GiftCard account when redeemed. If an order exceeds the amount of the GiftCard, the balance must be paid with a credit card or other available payment method.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "50-physical-gift-card",
        //        AllowCustomerReviews = true,
        //        Price = 50M,
        //        IsGiftCard = true,
        //        GiftCardType = GiftCardType.Physical,
        //        IsShipEnabled = true,
        //        IsFreeShipping = true,
        //        DeliveryDateId = deliveryDate.Id,
        //        Weight = 1,
        //        Length = 1,
        //        Width = 1,
        //        Height = 1,
        //        ManageInventoryMethod = ManageInventoryMethod.DontManageStock,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Gift Cards"),
        //                DisplayOrder = 3,
        //            }
        //        }
        //    };
        //    allProducts.Add(product50GiftCard);
        //    product50GiftCard.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_50giftcart.jpeg"), "image/jpeg", pictureService.GetPictureSeName(product50GiftCard.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(product50GiftCard);





        //    var product100GiftCard = new Product
        //    {
        //        ProductType = ProductType.SimpleProduct,
        //        VisibleIndividually = true,
        //        Name = "$100 Physical Gift Card",
        //        ShortDescription = "$100 Gift Card. Gift Cards must be redeemed through our site Web site toward the purchase of eligible products.",
        //        FullDescription = "<p>Gift Cards must be redeemed through our site Web site toward the purchase of eligible products. Purchases are deducted from the GiftCard balance. Any unused balance will be placed in the recipient's GiftCard account when redeemed. If an order exceeds the amount of the GiftCard, the balance must be paid with a credit card or other available payment method.</p>",
        //        ProductTemplateId = productTemplateSimple.Id,
        //        //SeName = "100-physical-gift-card",
        //        AllowCustomerReviews = true,
        //        Price = 100M,
        //        IsGiftCard = true,
        //        GiftCardType = GiftCardType.Physical,
        //        IsShipEnabled = true,
        //        DeliveryDateId = deliveryDate.Id,
        //        Weight = 1,
        //        Length = 1,
        //        Width = 1,
        //        Height = 1,
        //        ManageInventoryMethod = ManageInventoryMethod.DontManageStock,
        //        OrderMinimumQuantity = 1,
        //        OrderMaximumQuantity = 10000,
        //        StockQuantity = 10000,
        //        NotifyAdminForQuantityBelow = 1,
        //        AllowBackInStockSubscriptions = false,
        //        Published = true,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        ProductCategories =
        //        {
        //            new ProductCategory
        //            {
        //                Category = _categoryRepository.Table.Single(c => c.Name == "Gift Cards"),
        //                DisplayOrder = 4,
        //            }
        //        }
        //    };
        //    allProducts.Add(product100GiftCard);
        //    product100GiftCard.ProductPictures.Add(new ProductPicture
        //    {
        //        Picture = pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "product_100giftcart.jpeg"), "image/jpeg", pictureService.GetPictureSeName(product100GiftCard.Name)),
        //        DisplayOrder = 1,
        //    });
        //    _productRepository.Insert(product100GiftCard);

        //    #endregion



        //    //search engine names
        //    foreach (var product in allProducts)
        //    {
        //        _urlRecordRepository.Insert(new UrlRecord
        //        {
        //            EntityId = product.Id,
        //            EntityName = "Product",
        //            LanguageId = 0,
        //            IsActive = true,
        //            Slug = product.ValidateSeName("", product.Name, true)
        //        });
        //    }


        //    #region Related Products

        //    //related products
        //    var relatedProducts = new List<RelatedProduct>
        //    {
        //        new RelatedProduct
        //        {
        //             ProductId1 = productFlowerGirlBracelet.Id,
        //             ProductId2 = productEngagementRing.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productFlowerGirlBracelet.Id,
        //             ProductId2 = productElegantGemstoneNecklace.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productEngagementRing.Id,
        //             ProductId2 = productFlowerGirlBracelet.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productEngagementRing.Id,
        //             ProductId2 = productElegantGemstoneNecklace.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productElegantGemstoneNecklace.Id,
        //             ProductId2 = productFlowerGirlBracelet.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productElegantGemstoneNecklace.Id,
        //             ProductId2 = productEngagementRing.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productIfYouWait.Id,
        //             ProductId2 = productNightVision.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productIfYouWait.Id,
        //             ProductId2 = productScienceAndFaith.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productNightVision.Id,
        //             ProductId2 = productIfYouWait.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productNightVision.Id,
        //             ProductId2 = productScienceAndFaith.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productPrideAndPrejudice.Id,
        //             ProductId2 = productFirstPrizePies.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productPrideAndPrejudice.Id,
        //             ProductId2 = productFahrenheit.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productFirstPrizePies.Id,
        //             ProductId2 = productPrideAndPrejudice.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productFirstPrizePies.Id,
        //             ProductId2 = productFahrenheit.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productFahrenheit.Id,
        //             ProductId2 = productFirstPrizePies.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productFahrenheit.Id,
        //             ProductId2 = productPrideAndPrejudice.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAsusN551JK.Id,
        //             ProductId2 = productLenovoThinkpad.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAsusN551JK.Id,
        //             ProductId2 = productAppleMacBookPro.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAsusN551JK.Id,
        //             ProductId2 = productSamsungSeries.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAsusN551JK.Id,
        //             ProductId2 = productHpSpectre.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLenovoThinkpad.Id,
        //             ProductId2 = productAsusN551JK.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLenovoThinkpad.Id,
        //             ProductId2 = productAppleMacBookPro.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLenovoThinkpad.Id,
        //             ProductId2 = productSamsungSeries.Id,
        //        },
        //         new RelatedProduct
        //        {
        //             ProductId1 = productLenovoThinkpad.Id,
        //             ProductId2 = productHpEnvy.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAppleMacBookPro.Id,
        //             ProductId2 = productLenovoThinkpad.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAppleMacBookPro.Id,
        //             ProductId2 = productSamsungSeries.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAppleMacBookPro.Id,
        //             ProductId2 = productAsusN551JK.Id,
        //        },
        //         new RelatedProduct
        //        {
        //             ProductId1 = productAppleMacBookPro.Id,
        //             ProductId2 = productHpSpectre.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHpSpectre.Id,
        //             ProductId2 = productLenovoThinkpad.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHpSpectre.Id,
        //             ProductId2 = productSamsungSeries.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHpSpectre.Id,
        //             ProductId2 = productAsusN551JK.Id,
        //        },
        //         new RelatedProduct
        //        {
        //             ProductId1 = productHpSpectre.Id,
        //             ProductId2 = productHpEnvy.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHpEnvy.Id,
        //             ProductId2 = productAsusN551JK.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHpEnvy.Id,
        //             ProductId2 = productAppleMacBookPro.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHpEnvy.Id,
        //             ProductId2 = productHpSpectre.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHpEnvy.Id,
        //             ProductId2 = productSamsungSeries.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productSamsungSeries.Id,
        //             ProductId2 = productAsusN551JK.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productSamsungSeries.Id,
        //             ProductId2 = productAppleMacBookPro.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productSamsungSeries.Id,
        //             ProductId2 = productHpEnvy.Id,
        //        },
        //         new RelatedProduct
        //        {
        //             ProductId1 = productSamsungSeries.Id,
        //             ProductId2 = productHpSpectre.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLeica.Id,
        //             ProductId2 = productHtcOneMini.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLeica.Id,
        //             ProductId2 = productNikonD5500DSLR.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLeica.Id,
        //             ProductId2 = productAppleICam.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLeica.Id,
        //             ProductId2 = productNokiaLumia.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHtcOne.Id,
        //             ProductId2 = productHtcOneMini.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHtcOne.Id,
        //             ProductId2 = productNokiaLumia.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHtcOne.Id,
        //             ProductId2 = productBeatsPill.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHtcOne.Id,
        //             ProductId2 = productPortableSoundSpeakers.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHtcOneMini.Id,
        //             ProductId2 = productHtcOne.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHtcOneMini.Id,
        //             ProductId2 = productNokiaLumia.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHtcOneMini.Id,
        //             ProductId2 = productBeatsPill.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productHtcOneMini.Id,
        //             ProductId2 = productPortableSoundSpeakers.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productNokiaLumia.Id,
        //             ProductId2 = productHtcOne.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productNokiaLumia.Id,
        //             ProductId2 = productHtcOneMini.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productNokiaLumia.Id,
        //             ProductId2 = productBeatsPill.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productNokiaLumia.Id,
        //             ProductId2 = productPortableSoundSpeakers.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAdidas.Id,
        //             ProductId2 = productLeviJeans.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAdidas.Id,
        //             ProductId2 = productNikeFloral.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAdidas.Id,
        //             ProductId2 = productNikeZoom.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productAdidas.Id,
        //             ProductId2 = productNikeTailwind.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLeviJeans.Id,
        //             ProductId2 = productAdidas.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLeviJeans.Id,
        //             ProductId2 = productNikeFloral.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLeviJeans.Id,
        //             ProductId2 = productNikeZoom.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLeviJeans.Id,
        //             ProductId2 = productNikeTailwind.Id,
        //        },

        //        new RelatedProduct
        //        {
        //             ProductId1 = productCustomTShirt.Id,
        //             ProductId2 = productLeviJeans.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productCustomTShirt.Id,
        //             ProductId2 = productNikeTailwind.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productCustomTShirt.Id,
        //             ProductId2 = productOversizedWomenTShirt.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productCustomTShirt.Id,
        //             ProductId2 = productObeyHat.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productDigitalStorm.Id,
        //             ProductId2 = productBuildComputer.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productDigitalStorm.Id,
        //             ProductId2 = productLenovoIdeaCentre.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productDigitalStorm.Id,
        //             ProductId2 = productLenovoThinkpad.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productDigitalStorm.Id,
        //             ProductId2 = productAppleMacBookPro.Id,
        //        },


        //        new RelatedProduct
        //        {
        //             ProductId1 = productLenovoIdeaCentre.Id,
        //             ProductId2 = productBuildComputer.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLenovoIdeaCentre.Id,
        //             ProductId2 = productDigitalStorm.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLenovoIdeaCentre.Id,
        //             ProductId2 = productLenovoThinkpad.Id,
        //        },
        //        new RelatedProduct
        //        {
        //             ProductId1 = productLenovoIdeaCentre.Id,
        //             ProductId2 = productAppleMacBookPro.Id,
        //        },
        //    };
        //    _relatedProductRepository.Insert(relatedProducts);

        //    #endregion


        //    #region Product Tags

        //    //product tags
        //    AddProductTag(product25GiftCard, "nice");
        //    AddProductTag(product25GiftCard, "gift");
        //    AddProductTag(productNikeTailwind, "cool");
        //    AddProductTag(productNikeTailwind, "apparel");
        //    AddProductTag(productNikeTailwind, "shirt");
        //    AddProductTag(productBeatsPill, "computer");
        //    AddProductTag(productBeatsPill, "cool");
        //    AddProductTag(productNikeFloral, "cool");
        //    AddProductTag(productNikeFloral, "shoes");
        //    AddProductTag(productNikeFloral, "apparel");
        //    AddProductTag(productAdobePhotoshop, "computer");
        //    AddProductTag(productAdobePhotoshop, "awesome");
        //    AddProductTag(productUniversalTabletCover, "computer");
        //    AddProductTag(productUniversalTabletCover, "cool");
        //    AddProductTag(productOversizedWomenTShirt, "cool");
        //    AddProductTag(productOversizedWomenTShirt, "apparel");
        //    AddProductTag(productOversizedWomenTShirt, "shirt");
        //    AddProductTag(productAppleMacBookPro, "compact");
        //    AddProductTag(productAppleMacBookPro, "awesome");
        //    AddProductTag(productAppleMacBookPro, "computer");
        //    AddProductTag(productAsusN551JK, "compact");
        //    AddProductTag(productAsusN551JK, "awesome");
        //    AddProductTag(productAsusN551JK, "computer");
        //    AddProductTag(productFahrenheit, "awesome");
        //    AddProductTag(productFahrenheit, "book");
        //    AddProductTag(productFahrenheit, "nice");
        //    AddProductTag(productHtcOne, "cell");
        //    AddProductTag(productHtcOne, "compact");
        //    AddProductTag(productHtcOne, "awesome");
        //    AddProductTag(productBuildComputer, "awesome");
        //    AddProductTag(productBuildComputer, "computer");
        //    AddProductTag(productNikonD5500DSLR, "cool");
        //    AddProductTag(productNikonD5500DSLR, "camera");
        //    AddProductTag(productLeica, "camera");
        //    AddProductTag(productLeica, "cool");
        //    AddProductTag(productDigitalStorm, "cool");
        //    AddProductTag(productDigitalStorm, "computer");
        //    AddProductTag(productWindows8Pro, "awesome");
        //    AddProductTag(productWindows8Pro, "computer");
        //    AddProductTag(productCustomTShirt, "cool");
        //    AddProductTag(productCustomTShirt, "shirt");
        //    AddProductTag(productCustomTShirt, "apparel");
        //    AddProductTag(productElegantGemstoneNecklace, "jewelry");
        //    AddProductTag(productElegantGemstoneNecklace, "awesome");
        //    AddProductTag(productFlowerGirlBracelet, "awesome");
        //    AddProductTag(productFlowerGirlBracelet, "jewelry");
        //    AddProductTag(productFirstPrizePies, "book");
        //    AddProductTag(productAdidas, "cool");
        //    AddProductTag(productAdidas, "shoes");
        //    AddProductTag(productAdidas, "apparel");
        //    AddProductTag(productLenovoIdeaCentre, "awesome");
        //    AddProductTag(productLenovoIdeaCentre, "computer");
        //    AddProductTag(productSamsungSeries, "nice");
        //    AddProductTag(productSamsungSeries, "computer");
        //    AddProductTag(productSamsungSeries, "compact");
        //    AddProductTag(productHpSpectre, "nice");
        //    AddProductTag(productHpSpectre, "computer");
        //    AddProductTag(productHpEnvy, "computer");
        //    AddProductTag(productHpEnvy, "cool");
        //    AddProductTag(productHpEnvy, "compact");
        //    AddProductTag(productObeyHat, "apparel");
        //    AddProductTag(productObeyHat, "cool");
        //    AddProductTag(productLeviJeans, "cool");
        //    AddProductTag(productLeviJeans, "jeans");
        //    AddProductTag(productLeviJeans, "apparel");
        //    AddProductTag(productSoundForge, "game");
        //    AddProductTag(productSoundForge, "computer");
        //    AddProductTag(productSoundForge, "cool");
        //    AddProductTag(productNightVision, "awesome");
        //    AddProductTag(productNightVision, "digital");
        //    AddProductTag(productSunglasses, "apparel");
        //    AddProductTag(productSunglasses, "cool");
        //    AddProductTag(productHtcOneMini, "awesome");
        //    AddProductTag(productHtcOneMini, "compact");
        //    AddProductTag(productHtcOneMini, "cell");
        //    AddProductTag(productIfYouWait, "digital");
        //    AddProductTag(productIfYouWait, "awesome");
        //    AddProductTag(productNokiaLumia, "awesome");
        //    AddProductTag(productNokiaLumia, "cool");
        //    AddProductTag(productNokiaLumia, "camera");
        //    AddProductTag(productScienceAndFaith, "digital");
        //    AddProductTag(productScienceAndFaith, "awesome");
        //    AddProductTag(productPrideAndPrejudice, "book");
        //    AddProductTag(productLenovoThinkpad, "awesome");
        //    AddProductTag(productLenovoThinkpad, "computer");
        //    AddProductTag(productLenovoThinkpad, "compact");
        //    AddProductTag(productNikeZoom, "jeans");
        //    AddProductTag(productNikeZoom, "cool");
        //    AddProductTag(productNikeZoom, "apparel");
        //    AddProductTag(productEngagementRing, "jewelry");
        //    AddProductTag(productEngagementRing, "awesome");


        //    #endregion

        //    //reviews
        //    var random = new Random();
        //    foreach (var product in allProducts)
        //    {
        //        if (product.ProductType != ProductType.SimpleProduct)
        //            continue;

        //        //only 3 of 4 products will have reviews
        //        if (random.Next(4) == 3)
        //            continue;

        //        //rating from 4 to 5
        //        var rating = random.Next(4, 6);
        //        product.ProductReviews.Add(new ProductReview
        //        {
        //            CustomerId = defaultCustomer.Id,
        //            ProductId = product.Id,
        //            IsApproved = true,
        //            Title = "Some sample review",
        //            ReviewText = string.Format("This sample review is for the {0}. I've been waiting for this product to be available. It is priced just right.", product.Name),
        //            //random (4 or 5)
        //            Rating = rating,
        //            HelpfulYesTotal = 0,
        //            HelpfulNoTotal = 0,
        //            CreatedOn = DateTime.UtcNow
        //        });
        //        product.ApprovedRatingSum = rating;
        //        product.ApprovedTotalReviews = product.ProductReviews.Count;

        //    }
        //    _productRepository.Update(allProducts);
        //}

        //protected virtual void InstallForums()
        //{
        //    var forumGroup = new ForumGroup
        //    {
        //        Name = "General",
        //        DisplayOrder = 5,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //    };

        //    _forumGroupRepository.Insert(forumGroup);

        //    var newProductsForum = new Forum
        //    {
        //        ForumGroup = forumGroup,
        //        Name = "New Products",
        //        Description = "Discuss new products and industry trends",
        //        NumTopics = 0,
        //        NumPosts = 0,
        //        LastPostCustomerId = 0,
        //        LastPostTime = null,
        //        DisplayOrder = 1,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //    };
        //    _forumRepository.Insert(newProductsForum);

        //    var mobileDevicesForum = new Forum
        //    {
        //        ForumGroup = forumGroup,
        //        Name = "Mobile Devices Forum",
        //        Description = "Discuss the mobile phone market",
        //        NumTopics = 0,
        //        NumPosts = 0,
        //        LastPostCustomerId = 0,
        //        LastPostTime = null,
        //        DisplayOrder = 10,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //    };
        //    _forumRepository.Insert(mobileDevicesForum);

        //    var packagingShippingForum = new Forum
        //    {
        //        ForumGroup = forumGroup,
        //        Name = "Packaging & Shipping",
        //        Description = "Discuss packaging & shipping",
        //        NumTopics = 0,
        //        NumPosts = 0,
        //        LastPostTime = null,
        //        DisplayOrder = 20,
        //        CreatedOn = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //    };
        //    _forumRepository.Insert(packagingShippingForum);
        //}

        //protected virtual void InstallDiscounts()
        //{
        //    var discounts = new List<Discount>
        //                        {
        //                            new Discount
        //                                {
        //                                    Name = "Sample discount with coupon code",
        //                                    DiscountType = DiscountType.AssignedToSkus,
        //                                    DiscountLimitation = DiscountLimitationType.Unlimited,
        //                                    UsePercentage = false,
        //                                    DiscountAmount = 10,
        //                                    RequiresCouponCode = true,
        //                                    CouponCode = "123",
        //                                },
        //                            new Discount
        //                                {
        //                                    Name = "'20% order total' discount",
        //                                    DiscountType = DiscountType.AssignedToOrderTotal,
        //                                    DiscountLimitation = DiscountLimitationType.Unlimited,
        //                                    UsePercentage = true,
        //                                    DiscountPercentage = 20,
        //                                    StartDateUtc = new DateTime(2010,1,1),
        //                                    EndDateUtc = new DateTime(2020,1,1),
        //                                    RequiresCouponCode = true,
        //                                    CouponCode = "456",
        //                                },
        //                        };
        //    _discountRepository.Insert(discounts);
        //}

        //protected virtual void InstallBlogPosts()
        //{
        //    var defaultLanguage = _languageRepository.Table.FirstOrDefault();
        //    var blogPosts = new List<BlogPost>
        //                        {
        //                            new BlogPost
        //                                {
        //                                     AllowComments = true,
        //                                     Language = defaultLanguage,
        //                                     Title = "How a blog can help your growing e-Commerce business",
        //                                     BodyOverview = "<p>When you start an online business, your main aim is to sell the products, right? As a business owner, you want to showcase your store to more audience. So, you decide to go on social media, why? Because everyone is doing it, then why shouldn&rsquo;t you? It is tempting as everyone is aware of the hype that it is the best way to market your brand.</p><p>Do you know having a blog for your online store can be very helpful? Many businesses do not understand the importance of having a blog because they don&rsquo;t have time to post quality content.</p><p>Today, we will talk about how a blog can play an important role for the growth of your e-Commerce business. Later, we will also discuss some tips that will be helpful to you for writing business related blog posts.</p>",
        //                                     Body = "<p>When you start an online business, your main aim is to sell the products, right? As a business owner, you want to showcase your store to more audience. So, you decide to go on social media, why? Because everyone is doing it, then why shouldn&rsquo;t you? It is tempting as everyone is aware of the hype that it is the best way to market your brand.</p><p>Do you know having a blog for your online store can be very helpful? Many businesses do not understand the importance of having a blog because they don&rsquo;t have time to post quality content.</p><p>Today, we will talk about how a blog can play an important role for the growth of your e-Commerce business. Later, we will also discuss some tips that will be helpful to you for writing business related blog posts.</p><h3>1) Blog is useful in educating your customers</h3><p>Blogging is one of the best way by which you can educate your customers about your products/services that you offer. This helps you as a business owner to bring more value to your brand. When you provide useful information to the customers about your products, they are more likely to buy products from you. You can use your blog for providing tutorials in regard to the use of your products.</p><p><strong>For example:</strong> If you have an online store that offers computer parts. You can write tutorials about how to build a computer or how to make your computer&rsquo;s performance better. While talking about these things, you can mention products in the tutorials and provide link to your products within the blog post from your website. Your potential customers might get different ideas of using your product and will likely to buy products from your online store.</p><h3>2) Blog helps your business in Search Engine Optimization (SEO)</h3><p>Blog posts create more internal links to your website which helps a lot in SEO. Blog is a great way to have quality content on your website related to your products/services which is indexed by all major search engines like Google, Bing and Yahoo. The more original content you write in your blog post, the better ranking you will get in search engines. SEO is an on-going process and posting blog posts regularly keeps your site active all the time which is beneficial when it comes to search engine optimization.</p><p><strong>For example:</strong> Let&rsquo;s say you sell &ldquo;Sony Television Model XYZ&rdquo; and you regularly publish blog posts about your product. Now, whenever someone searches for &ldquo;Sony Television Model XYZ&rdquo;, Google will crawl on your website knowing that you have something to do with this particular product. Hence, your website will show up on the search result page whenever this item is being searched.</p><h3>3) Blog helps in boosting your sales by convincing the potential customers to buy</h3><p>If you own an online business, there are so many ways you can share different stories with your audience in regard your products/services that you offer. Talk about how you started your business, share stories that educate your audience about what&rsquo;s new in your industry, share stories about how your product/service was beneficial to someone or share anything that you think your audience might find interesting (it does not have to be related to your product). This kind of blogging shows that you are an expert in your industry and interested in educating your audience. It sets you apart in the competitive market. This gives you an opportunity to showcase your expertise by educating the visitors and it can turn your audience into buyers.</p><p><strong>Fun Fact:</strong> Did you know that 92% of companies who decided to blog acquired customers through their blog?</p><p><a href=\"http://www.nopcommerce.com/\">nopCommerce</a> is great e-Commerce solution that also offers a variety of CMS features including blog. A store owner has full access for managing the blog posts and related comments.</p>",
        //                                     Tags = "e-commerce, blog, moey",
        //                                     CreatedOn = DateTime.UtcNow,
        //                                },
        //                            new BlogPost
        //                                {
        //                                     AllowComments = true,
        //                                     Language = defaultLanguage,
        //                                     Title = "Why your online store needs a wish list",
        //                                     BodyOverview = "<p>What comes to your mind, when you hear the term&rdquo; wish list&rdquo;? The application of this feature is exactly how it sounds like: a list of things that you wish to get. As an online store owner, would you like your customers to be able to save products in a wish list so that they review or buy them later? Would you like your customers to be able to share their wish list with friends and family for gift giving?</p><p>Offering your customers a feature of wish list as part of shopping cart is a great way to build loyalty to your store site. Having the feature of wish list on a store site allows online businesses to engage with their customers in a smart way as it allows the shoppers to create a list of what they desire and their preferences for future purchase.</p>",
        //                                     Body = "<p>What comes to your mind, when you hear the term&rdquo; wish list&rdquo;? The application of this feature is exactly how it sounds like: a list of things that you wish to get. As an online store owner, would you like your customers to be able to save products in a wish list so that they review or buy them later? Would you like your customers to be able to share their wish list with friends and family for gift giving?</p><p>Offering your customers a feature of wish list as part of shopping cart is a great way to build loyalty to your store site. Having the feature of wish list on a store site allows online businesses to engage with their customers in a smart way as it allows the shoppers to create a list of what they desire and their preferences for future purchase.</p><p>Does every e-Commerce store needs a wish list? The answer to this question in most cases is yes, because of the following reasons:</p><p><strong>Understanding the needs of your customers</strong> - A wish list is a great way to know what is in your customer&rsquo;s mind. Try to think the purchase history as a small portion of the customer&rsquo;s preferences. But, the wish list is like a wide open door that can give any online business a lot of valuable information about their customer and what they like or desire.</p><p><strong>Shoppers like to share their wish list with friends and family</strong> - Providing your customers a way to email their wish list to their friends and family is a pleasant way to make online shopping enjoyable for the shoppers. It is always a good idea to make the wish list sharable by a unique link so that it can be easily shared though different channels like email or on social media sites.</p><p><strong>Wish list can be a great marketing tool</strong> &ndash; Another way to look at wish list is a great marketing tool because it is extremely targeted and the recipients are always motivated to use it. For example: when your younger brother tells you that his wish list is on a certain e-Commerce store. What is the first thing you are going to do? You are most likely to visit the e-Commerce store, check out the wish list and end up buying something for your younger brother.</p><p>So, how a wish list is a marketing tool? The reason is quite simple, it introduce your online store to new customers just how it is explained in the above example.</p><p><strong>Encourage customers to return to the store site</strong> &ndash; Having a feature of wish list on the store site can increase the return traffic because it encourages customers to come back and buy later. Allowing the customers to save the wish list to their online accounts gives them a reason return to the store site and login to the account at any time to view or edit the wish list items.</p><p><strong>Wish list can be used for gifts for different occasions like weddings or birthdays. So, what kind of benefits a gift-giver gets from a wish list?</strong></p><ul><li>It gives them a surety that they didn&rsquo;t buy a wrong gift</li><li>It guarantees that the recipient will like the gift</li><li>It avoids any awkward moments when the recipient unwraps the gift and as a gift-giver you got something that the recipient do not want</li></ul><p><strong>Wish list is a great feature to have on a store site &ndash; So, what kind of benefits a business owner gets from a wish list</strong></p><ul><li>It is a great way to advertise an online store as many people do prefer to shop where their friend or family shop online</li><li>It allows the current customers to return to the store site and open doors for the new customers</li><li>It allows store admins to track what&rsquo;s in customers wish list and run promotions accordingly to target specific customer segments</li></ul><p><a href=\"http://www.nopcommerce.com/\">nopCommerce</a> offers the feature of wish list that allows customers to create a list of products that they desire or planning to buy in future.</p>",
        //                                     Tags = "e-commerce, nopCommerce, sample tag, money",
        //                                     CreatedOn = DateTime.UtcNow.AddSeconds(1),
        //                                },
        //                        };
        //    _blogPostRepository.Insert(blogPosts);

        //    //search engine names
        //    foreach (var blogPost in blogPosts)
        //    {
        //        _urlRecordRepository.Insert(new UrlRecord
        //        {
        //            EntityId = blogPost.Id,
        //            EntityName = "BlogPost",
        //            LanguageId = blogPost.LanguageId,
        //            IsActive = true,
        //            Slug = blogPost.ValidateSeName("", blogPost.Title, true)
        //        });
        //    }
        //}

        //protected virtual void InstallNews()
        //{
        //    var defaultLanguage = _languageRepository.Table.FirstOrDefault();
        //    var news = new List<NewsItem>
        //                        {
        //                            new NewsItem
        //                            {
        //                                 AllowComments = true,
        //                                 Language = defaultLanguage,
        //                                 Title = "About nopCommerce",
        //                                 Short = "It's stable and highly usable. From downloads to documentation, www.nopCommerce.com offers a comprehensive base of information, resources, and support to the nopCommerce community.",
        //                                 Full = "<p>For full feature list go to <a href=\"http://www.nopCommerce.com\">nopCommerce.com</a></p><p>Providing outstanding custom search engine optimization, web development services and e-commerce development solutions to our clients at a fair price in a professional manner.</p>",
        //                                 Published  = true,
        //                                 CreatedOn = DateTime.UtcNow,
        //                            },
        //                            new NewsItem
        //                            {
        //                                 AllowComments = true,
        //                                 Language = defaultLanguage,
        //                                 Title = "nopCommerce new release!",
        //                                 Short = "nopCommerce includes everything you need to begin your e-commerce online store. We have thought of everything and it's all included! nopCommerce is a fully customizable shopping cart",
        //                                 Full = "<p>nopCommerce includes everything you need to begin your e-commerce online store. We have thought of everything and it's all included!</p>",
        //                                 Published  = true,
        //                                 CreatedOn = DateTime.UtcNow.AddSeconds(1),
        //                            },
        //                            new NewsItem
        //                            {
        //                                 AllowComments = true,
        //                                 Language = defaultLanguage,
        //                                 Title = "New online store is open!",
        //                                 Short = "The new nopCommerce store is open now! We are very excited to offer our new range of products. We will be constantly adding to our range so please register on our site.",
        //                                 Full = "<p>Our online store is officially up and running. Stock up for the holiday season! We have a great selection of items. We will be constantly adding to our range so please register on our site, this will enable you to keep up to date with any new products.</p><p>All shipping is worldwide and will leave the same day an order is placed! Happy Shopping and spread the word!!</p>",
        //                                 Published  = true,
        //                                 CreatedOn = DateTime.UtcNow.AddSeconds(2),
        //                            },

        //                        };
        //    _newsItemRepository.Insert(news);

        //    //search engine names
        //    foreach (var newsItem in news)
        //    {
        //        _urlRecordRepository.Insert(new UrlRecord
        //        {
        //            EntityId = newsItem.Id,
        //            EntityName = "NewsItem",
        //            LanguageId = newsItem.LanguageId,
        //            IsActive = true,
        //            Slug = newsItem.ValidateSeName("", newsItem.Title, true)
        //        });
        //    }
        //}

        //protected virtual void InstallPolls()
        //{
        //    var defaultLanguage = _languageRepository.Table.FirstOrDefault();
        //    var poll1 = new Poll
        //    {
        //        Language = defaultLanguage,
        //        Name = "Do you like nopCommerce?",
        //        SystemKeyword = "",
        //        Published = true,
        //        ShowOnHomePage = true,
        //        DisplayOrder = 1,
        //    };
        //    poll1.PollAnswers.Add(new PollAnswer
        //    {
        //        Name = "Excellent",
        //        DisplayOrder = 1,
        //    });
        //    poll1.PollAnswers.Add(new PollAnswer
        //    {
        //        Name = "Good",
        //        DisplayOrder = 2,
        //    });
        //    poll1.PollAnswers.Add(new PollAnswer
        //    {
        //        Name = "Poor",
        //        DisplayOrder = 3,
        //    });
        //    poll1.PollAnswers.Add(new PollAnswer
        //    {
        //        Name = "Very bad",
        //        DisplayOrder = 4,
        //    });
        //    _pollRepository.Insert(poll1);
        //}

        //protected virtual void InstallActivityLogTypes()
        //{
        //    var activityLogTypes = new List<ActivityLogType>
        //                              {
        //                                  //admin area activities
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewCategory",
        //                                          Enabled = true,
        //                                          Name = "Add a new category"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewCheckoutAttribute",
        //                                          Enabled = true,
        //                                          Name = "Add a new checkout attribute"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewCustomer",
        //                                          Enabled = true,
        //                                          Name = "Add a new customer"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewCustomerRole",
        //                                          Enabled = true,
        //                                          Name = "Add a new customer role"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewDiscount",
        //                                          Enabled = true,
        //                                          Name = "Add a new discount"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewGiftCard",
        //                                          Enabled = true,
        //                                          Name = "Add a new gift card"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewManufacturer",
        //                                          Enabled = true,
        //                                          Name = "Add a new manufacturer"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewProduct",
        //                                          Enabled = true,
        //                                          Name = "Add a new product"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewProductAttribute",
        //                                          Enabled = true,
        //                                          Name = "Add a new product attribute"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewSetting",
        //                                          Enabled = true,
        //                                          Name = "Add a new setting"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewSpecAttribute",
        //                                          Enabled = true,
        //                                          Name = "Add a new specification attribute"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "AddNewWidget",
        //                                          Enabled = true,
        //                                          Name = "Add a new widget"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteCategory",
        //                                          Enabled = true,
        //                                          Name = "Delete category"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteCheckoutAttribute",
        //                                          Enabled = true,
        //                                          Name = "Delete a checkout attribute"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteCustomer",
        //                                          Enabled = true,
        //                                          Name = "Delete a customer"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteCustomerRole",
        //                                          Enabled = true,
        //                                          Name = "Delete a customer role"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteDiscount",
        //                                          Enabled = true,
        //                                          Name = "Delete a discount"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteGiftCard",
        //                                          Enabled = true,
        //                                          Name = "Delete a gift card"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteManufacturer",
        //                                          Enabled = true,
        //                                          Name = "Delete a manufacturer"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteProduct",
        //                                          Enabled = true,
        //                                          Name = "Delete a product"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteProductAttribute",
        //                                          Enabled = true,
        //                                          Name = "Delete a product attribute"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteReturnRequest",
        //                                          Enabled = true,
        //                                          Name = "Delete a return request"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteSetting",
        //                                          Enabled = true,
        //                                          Name = "Delete a setting"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteSpecAttribute",
        //                                          Enabled = true,
        //                                          Name = "Delete a specification attribute"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "DeleteWidget",
        //                                          Enabled = true,
        //                                          Name = "Delete a widget"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditCategory",
        //                                          Enabled = true,
        //                                          Name = "Edit category"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditCheckoutAttribute",
        //                                          Enabled = true,
        //                                          Name = "Edit a checkout attribute"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditCustomer",
        //                                          Enabled = true,
        //                                          Name = "Edit a customer"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditCustomerRole",
        //                                          Enabled = true,
        //                                          Name = "Edit a customer role"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditDiscount",
        //                                          Enabled = true,
        //                                          Name = "Edit a discount"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditGiftCard",
        //                                          Enabled = true,
        //                                          Name = "Edit a gift card"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditManufacturer",
        //                                          Enabled = true,
        //                                          Name = "Edit a manufacturer"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditProduct",
        //                                          Enabled = true,
        //                                          Name = "Edit a product"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditProductAttribute",
        //                                          Enabled = true,
        //                                          Name = "Edit a product attribute"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditPromotionProviders",
        //                                          Enabled = true,
        //                                          Name = "Edit promotion providers"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditReturnRequest",
        //                                          Enabled = true,
        //                                          Name = "Edit a return request"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditSettings",
        //                                          Enabled = true,
        //                                          Name = "Edit setting(s)"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditSpecAttribute",
        //                                          Enabled = true,
        //                                          Name = "Edit a specification attribute"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "EditWidget",
        //                                          Enabled = true,
        //                                          Name = "Edit a widget"
        //                                      },
        //                                      //public store activities
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.ViewCategory",
        //                                          Enabled = false,
        //                                          Name = "Public store. View a category"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.ViewManufacturer",
        //                                          Enabled = false,
        //                                          Name = "Public store. View a manufacturer"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.ViewProduct",
        //                                          Enabled = false,
        //                                          Name = "Public store. View a product"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.PlaceOrder",
        //                                          Enabled = false,
        //                                          Name = "Public store. Place an order"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.SendPM",
        //                                          Enabled = false,
        //                                          Name = "Public store. Send PM"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.ContactUs",
        //                                          Enabled = false,
        //                                          Name = "Public store. Use contact us form"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.AddToCompareList",
        //                                          Enabled = false,
        //                                          Name = "Public store. Add to compare list"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.AddToShoppingCart",
        //                                          Enabled = false,
        //                                          Name = "Public store. Add to shopping cart"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.AddToWishlist",
        //                                          Enabled = false,
        //                                          Name = "Public store. Add to wishlist"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.Login",
        //                                          Enabled = false,
        //                                          Name = "Public store. Login"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.Logout",
        //                                          Enabled = false,
        //                                          Name = "Public store. Logout"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.AddProductReview",
        //                                          Enabled = false,
        //                                          Name = "Public store. Add product review"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.AddNewsComment",
        //                                          Enabled = false,
        //                                          Name = "Public store. Add news comment"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.AddBlogComment",
        //                                          Enabled = false,
        //                                          Name = "Public store. Add blog comment"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.AddForumTopic",
        //                                          Enabled = false,
        //                                          Name = "Public store. Add forum topic"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.EditForumTopic",
        //                                          Enabled = false,
        //                                          Name = "Public store. Edit forum topic"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.DeleteForumTopic",
        //                                          Enabled = false,
        //                                          Name = "Public store. Delete forum topic"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.AddForumPost",
        //                                          Enabled = false,
        //                                          Name = "Public store. Add forum post"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.EditForumPost",
        //                                          Enabled = false,
        //                                          Name = "Public store. Edit forum post"
        //                                      },
        //                                  new ActivityLogType
        //                                      {
        //                                          SystemKeyword = "PublicStore.DeleteForumPost",
        //                                          Enabled = false,
        //                                          Name = "Public store. Delete forum post"
        //                                      },
        //                              };
        //    _activityLogTypeRepository.Insert(activityLogTypes);
        //}

        //protected virtual void InstallProductTemplates()
        //{
        //    var productTemplates = new List<ProductTemplate>
        //                       {
        //                           new ProductTemplate
        //                               {
        //                                   Name = "Simple product",
        //                                   ViewPath = "ProductTemplate.Simple",
        //                                   DisplayOrder = 10
        //                               },
        //                           new ProductTemplate
        //                               {
        //                                   Name = "Grouped product",
        //                                   ViewPath = "ProductTemplate.Grouped",
        //                                   DisplayOrder = 100
        //                               },
        //                       };
        //    _productTemplateRepository.Insert(productTemplates);
        //}

        //protected virtual void InstallCategoryTemplates()
        //{
        //    var categoryTemplates = new List<CategoryTemplate>
        //                       {
        //                           new CategoryTemplate
        //                               {
        //                                   Name = "Products in Grid or Lines",
        //                                   ViewPath = "CategoryTemplate.ProductsInGridOrLines",
        //                                   DisplayOrder = 1
        //                               },
        //                       };
        //    _categoryTemplateRepository.Insert(categoryTemplates);
        //}

        //protected virtual void InstallManufacturerTemplates()
        //{
        //    var manufacturerTemplates = new List<ManufacturerTemplate>
        //                       {
        //                           new ManufacturerTemplate
        //                               {
        //                                   Name = "Products in Grid or Lines",
        //                                   ViewPath = "ManufacturerTemplate.ProductsInGridOrLines",
        //                                   DisplayOrder = 1
        //                               },
        //                       };
        //    _manufacturerTemplateRepository.Insert(manufacturerTemplates);
        //}

        //protected virtual void InstallTopicTemplates()
        //{
        //    var topicTemplates = new List<TopicTemplate>
        //                       {
        //                           new TopicTemplate
        //                               {
        //                                   Name = "Default template",
        //                                   ViewPath = "TopicDetails",
        //                                   DisplayOrder = 1
        //                               },
        //                       };
        //    _topicTemplateRepository.Insert(topicTemplates);
        //}

        //protected virtual void InstallScheduleTasks()
        //{
        //    var tasks = new List<ScheduleTask>
        //    {
        //        new ScheduleTask
        //        {
        //            Name = "Send emails",
        //            Seconds = 60,
        //            Type = "Nop.Services.Messages.QueuedMessagesSendTask, Nop.Services",
        //            Enabled = true,
        //            StopOnError = false,
        //        },
        //        new ScheduleTask
        //        {
        //            Name = "Keep alive",
        //            Seconds = 300,
        //            Type = "Nop.Services.Common.KeepAliveTask, Nop.Services",
        //            Enabled = true,
        //            StopOnError = false,
        //        },
        //        new ScheduleTask
        //        {
        //            Name = "Delete guests",
        //            Seconds = 600,
        //            Type = "Nop.Services.Customers.DeleteGuestsTask, Nop.Services",
        //            Enabled = true,
        //            StopOnError = false,
        //        },
        //        new ScheduleTask
        //        {
        //            Name = "Clear cache",
        //            Seconds = 600,
        //            Type = "Nop.Services.Caching.ClearCacheTask, Nop.Services",
        //            Enabled = false,
        //            StopOnError = false,
        //        },
        //        new ScheduleTask
        //        {
        //            Name = "Clear log",
        //            //60 minutes
        //            Seconds = 3600,
        //            Type = "Nop.Services.Logging.ClearLogTask, Nop.Services",
        //            Enabled = false,
        //            StopOnError = false,
        //        },
        //        new ScheduleTask
        //        {
        //            Name = "Update currency exchange rates",
        //            Seconds = 900,
        //            Type = "Nop.Services.Directory.UpdateExchangeRateTask, Nop.Services",
        //            Enabled = true,
        //            StopOnError = false,
        //        },
        //    };

        //    _scheduleTaskRepository.Insert(tasks);
        //}

        //private void AddProductTag(Product product, string tag)
        //{
        //    var productTag = _productTagRepository.Table.FirstOrDefault(pt => pt.Name == tag);
        //    if (productTag == null)
        //    {
        //        productTag = new ProductTag
        //        {
        //            Name = tag,
        //        };
        //    }
        //    product.ProductTags.Add(productTag);
        //    _productRepository.Update(product);
        //}

        #endregion

        #region Methods

        public virtual void InstallData(string defaultUserEmail,
            string defaultUserPassword, bool installSampleData = true)
        {
           
            //InstallCustomersAndUsers(defaultUserEmail, defaultUserPassword);

            //InstallEmailAccounts();
            //InstallMessageTemplates();
            //InstallSettings();
            //InstallTopicTemplates();
            //InstallTopics();
            //InstallLocaleResources();
            //InstallActivityLogTypes();
            //HashDefaultCustomerPassword(defaultUserEmail, defaultUserPassword);
            //InstallProductTemplates();
            //InstallCategoryTemplates();
            //InstallManufacturerTemplates();
            //InstallScheduleTasks();

            //if (installSampleData)
            //{
            //    InstallCheckoutAttributes();
            //    InstallSpecificationAttributes();
            //    InstallProductAttributes();
            //    InstallCategories();
            //    InstallManufacturers();
            //    InstallProducts(defaultUserEmail);
            //    InstallForums();
            //    InstallDiscounts();
            //    InstallBlogPosts();
            //    InstallNews();
            //    InstallPolls();
            //}
        }

        #endregion
    }
}
