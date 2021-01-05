<template>
    	<!-- WRAPPER -->
    <div>
        <div class="form-group">
            <div class="col-xs-12">
                <input type="text" name="UserName" class="form-control" id="username" placeholder="UserName" required=required>
                <span validation-for="UserName"></span>
            </div>
        </div>
        <div class="form-group">
            <div class="col-xs-12">
                <input id="input" placeholder="Password" class="form-control" type="password" name="Password" required=required>
            </div>
        </div>
        <input type="hidden" v-if="FundTypeList.length == 1" name="fundtype" :value="postBody.FundTypeID" />

        <div class="form-group" v-if="FundTypeList.length > 1">
            <div class="col-xs-12">
                <select class="form-control" v-model="postBody.FundTypeID" name="fundtype" required>
                    <option v-for="fundtype in FundTypeList" v-bind:value="fundtype.id" v-bind:key="fundtype.id"> {{ fundtype.description }} </option>
                </select>


            </div>
        </div>
        <div class="form-group row">
            <div class="col-md-12">
                <div class="checkbox checkbox-primary float-left p-t-0">
                    <input id="checkbox-signup" type="checkbox" class="filled-in chk-col-light-blue">
                    <label for="checkbox-signup"> Remember me </label>
                </div>
                <a href="/Home/ForgotPassword" id="to-recover" class="text-muted float-right"><i class="fa fa-lock m-r-5"></i> Forgot pwd?</a>
            </div>
        </div>
        <div class="form-group text-center m-t-20">
            <div class="col-xs-12">

                <button type="submit" class="btn btn-primary btn-lg btn-block">LOGIN</button>
            </div>
        </div>
    </div>
							
	<!-- END WRAPPER -->
</template>

<script>
export default {
    props: ["privacyLink", "cookieString"],
    data() {
        return {
            hidden: false,
            FundTypeList:[],
             postBody: {
                FundTypeID:'',
            }
        };
    },
    methods: {
        validateUser(){
			
		}
        },

     mounted () {
        axios
            .get('/api/FundTypeCode/getAllFundTypeCodes')
            .then(response => {
                this.FundTypeList = response.data
                
                if(this.FundTypeList.length === 1){
                this.postBody.FundTypeID = this.FundTypeList[0].code
                }
                
                
                })

            
        }
    };
     
</script>