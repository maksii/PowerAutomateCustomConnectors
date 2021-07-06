# Cloudinary Connector

![image](https://user-images.githubusercontent.com/1761348/124588427-74573b80-de61-11eb-922d-8a8761ec4f7b.png)

Manage images in the cloud, edit and transform on the fly, and optimize performance on any screen using Cloudinary’s API Custom Connector.
Basic connector to upload image with preset transformation into the Cloudinary.

      Upload Action
      Require The Data URI (Base64 encoded) image as input and Preset to be used.
      
      Search Action
      Reqire search criteria, eg tags=DataverseUpload to return array of associated images

Connector currently configured to support oub of the box Preset configuration for image transofrmation. That mean, that you will access:
1. Image extension conversion
2. Cropping and resing
3. Thumbnail generation
4. Qualiti(size) reduction
5. and a lot more

![image](https://user-images.githubusercontent.com/1761348/124631731-56530080-de8c-11eb-8dc7-1fcdbe870d77.png)
![image](https://user-images.githubusercontent.com/1761348/124631785-636fef80-de8c-11eb-9a2d-333d7df70a45.png)
![image](https://user-images.githubusercontent.com/1761348/124631862-771b5600-de8c-11eb-9c80-4004c49c49cc.png)

Example:
![image](https://user-images.githubusercontent.com/1761348/124634127-a763f400-de8e-11eb-941a-db993a4a2625.png)


# Pricing
![image](https://user-images.githubusercontent.com/1761348/124632168-c19cd280-de8c-11eb-9f68-711d13897db0.png)
Free Tier provides with 25000 transformations. Keep in mind, that single Action could contains multiple Transformation(eg, crop+extension+thumbnail=3 transformations)

# Setup

Register and Login into you https://cloudinary.com/ panel
       
       Copy Cloud name, API Key and API Secret
       Follow base configuration and under /settings/upload create/update upload 'Unsigned' preset
       
       
       
# Note
Currently only 'Unsigned' upload available as part of this connector

Please report any issues or new future requests under Issues tab.

# Options
For Upload action provide clound name of your Cloudinary account and Preset that should be used. Folder, Public Id and tags - optional to use, but may be usefull for navigation in the future. Please check official documentation https://cloudinary.com/documentation/image_upload_api_reference
![image](https://user-images.githubusercontent.com/1761348/124586480-26d9cf00-de5f-11eb-9008-af01c1e5a67b.png)

Uploaded image will be as part of Action response at secure_url property.
![image](https://user-images.githubusercontent.com/1761348/124586555-41ac4380-de5f-11eb-9890-5108fd238c9b.png)

For Search action provide values of required tag to search after tag= value. Or replace condition with required one.
Please check official documentation https://cloudinary.com/documentation/search_api
![image](https://user-images.githubusercontent.com/1761348/124586517-33f6be00-de5f-11eb-98e9-10d9091ebf79.png)
![image](https://user-images.githubusercontent.com/1761348/124586606-4f61c900-de5f-11eb-8ab2-4fd3062c0186.png)
