<template>
    	<!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error" style="color:red;">
            <p v-for="error in errors" v-bind:value="error.index" v-bind:key="error.index">
                {{ error }}
            </p>
        </div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <div class="card-body">
            <div class="row">
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Type of Account</label>

                        <select class="form-control" v-model="postBody.maincode" @change="getLastUsedMainAccount" :disabled="submitorUpdate == 'Update'" name="maincode" required>
                            <option v-for="mainact in mainaccountcodes" v-bind:value="mainact.value" v-bind:key="mainact.value"> {{ mainact.text }} </option>
                        </select>

                    </div>
                    <div class="form-group">
                        <label class="form-label">Description</label>
                        <input class="form-control" name="description" v-model="postBody.description" placeholder="" />
                    </div>
                    <div class="form-group">
                        <label class="form-label">Short Name</label>
                        <input class="form-control" name="shortname" v-model="postBody.shortname" placeholder="" />
                    </div>
                </div>

                <div class="col-12 col-xl-6">

                    <div class="form-group">
                        <label class="form-label">Sub Type</label>

                        <select class="form-control" v-model="postBody.subtype" required>
                            <option v-for="subt in subtype" v-bind:value="subt.value" v-bind:key="subt.value"> {{ subt.text }} </option>
                        </select>

                    </div>

                       <div class="form-group">
                        <label class="form-label">Balance Sheet</label>

                        <select class="form-control" v-model="postBody.npf_balsheet_bl_code" name="mainAct_dd" required>
                            <option v-for="balSheet in balanceSheetList" v-bind:value="balSheet.bl_code" v-bind:key="balSheet.bl_code"> {{ balSheet.bl_desc }} </option>
                        </select>

                    </div>

                    <div class="form-group">
                        <label class="form-label">Preferred Code</label>
                        <input class="form-control" name="preferredCode" v-model="preferredCode" placeholder="0000" />
                    </div>

                    </div>
                <div class="col-12 col-xl-6">
                   <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button></div>
                </div>
               
            </div>

        </div>
        
    </div>
							
	<!-- END WRAPPER -->
</template>

<script>
export default {
    
    data() { 
        return {
        responseMessage:'',
        errors: null,
        new_maincode :'',
        preferredCode: '',
        submitorUpdate : 'Submit',
        balanceSheetList:null,
        canProcess : true,
        postBody: {
                maincode: '',
                description:'',
                subtype: '',
                shortname:'',
                npf_balsheet_bl_code:'',

        },
        mainaccountcodes: [
            { value: '10', text: 'Assets' },
            { value: '20', text: 'Liabilities' },
            { value: '30', text: 'Capital and Reserve' },
            { value: '40', text: 'Income' },
            { value: '50', text: 'Expenses' }
            ],
       subtype: [
           { value: '1', text: 'GL Account' },
           { value: '2', text: 'Customers' },
           { value: '4', text: 'Bank' },
           { value: '6', text: 'Mgt Expenses' },
           { value: '7', text: 'Tech Expenses' },
           { value: '8', text: 'Assets' },
           { value: '9', text: 'Income' },
           { value: '3', text: 'None' },
           { value: '5', text: 'None' }
    ]
        };
    },
     mounted () {
        axios
            .get('/api/BalanceSheet/getAllBalanceSheets')
             .then(response => (this.balanceSheetList = response.data))

      
     },
     
    watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) {
           
                this.postBody.maincode = this.$store.state.objectToUpdate.npf_balsheet_bl_code,
                this.postBody.description = this.$store.state.objectToUpdate.description,
                this.postBody.subtype = this.$store.state.objectToUpdate.subtype,
                this.postBody.shortname = this.$store.state.objectToUpdate.shortname,
                this.postBody.npf_balsheet_bl_code = this.$store.state.objectToUpdate.npf_balsheet_bl_code
                this.submitorUpdate = 'Update';
               
        }
    },
    methods: {
        checkForm: function (e) {
            
         if (this.postBody.maincode && this.postBody.subtype && this.postBody.npf_balsheet_bl_code) {
              e.preventDefault();
             this.canProcess = false;
             console.log(this.postBody.maincode, this.new_maincode);
             this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('Supply the Compulsory filde(s)');
          }
        },

        getLastUsedMainAccount() {
            axios.get(`/api/MainAccount/getLastMainAccount/${ this.postBody.maincode }`)
                        .then(response => {
                            
                            if (response.data.responseCode == '200' && response.data.data != 0) {
                                this.new_maincode = 0;
                                this.new_maincode = parseInt(response.data.data.maincode.slice(0, response.data.data.maincode.length)) + 100;
                                //this.new_maincode = this.postBody.maincode + ""+this.new_maincode;

                                alert(this.new_maincode);
                                //alert(parseInt(this.new_maincode) + 100);
                            }
                            else {
                                this.new_maincode = this.postBody.maincode + "10";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });

                        // this.preferredCode = this.new_maincode.slice(0, this.new_maincode.length -4)
        },
        postPost() {
            console.log(this.preferredCode);
            if (this.preferredCode !== null && this.preferredCode !== ""){
                    
                    if(this.preferredCode.length === 4){
                        
                        var firstDigit = this.preferredCode.slice(0, this.preferredCode.length -3)

                        if(firstDigit !== '1' && firstDigit !== '2' && firstDigit !== '3' && firstDigit !== '4' & firstDigit !== '5')
                        {
                            this.errors = [];
                            this.errors.push('The first number(' +firstDigit+ ') of your Preferred code is invalid!!!');

                            this.canProcess = true;
                        }
                        else{

                            if (this.submitorUpdate == 'Submit') {
                
                                this.postBody.maincode = this.preferredCode;
                                axios.post(`/api/MainAccount/createMainAccount`, this.postBody )
                                .then(response => {
                                this.responseMessage = response.data.responseDescription;
                                this.canProcess = true;
                                if (response.data.responseCode == '200')
                                {
                                    this.postBody.maincode = '';this.postBody.description = '';
                                    this.postBody.npf_balsheet_bl_code = ''; this.postBody.subtype = '';
                                    this.postBody.shortname = '';
                                    this.$store.state.objectToUpdate = "create";
                                    this.errors = [];
                                }
                                })
                                .catch(e => {
                                this.errors.push(e)
                                });
                            }
                        }

                    }
                    else{
                        
                        this.canProcess = true;
                        this.errors = [];
                        this.errors.push('Preferred code must be FOUR(4) digits!!!');
                    }
            }
            else
            {
                if (this.submitorUpdate == 'Submit') {
                
                this.postBody.maincode = this.new_maincode;
                    axios.post(`/api/MainAccount/createMainAccount`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if (response.data.responseCode == '200')
                            {
                                this.postBody.maincode = '';this.postBody.description = '';
                                this.postBody.npf_balsheet_bl_code = ''; this.postBody.subtype = '';
                                this.postBody.shortname = '';
                                this.$store.state.objectToUpdate = "create";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }

                if (this.submitorUpdate == 'Update') {
                     this.postBody.maincode = this.$store.state.objectToUpdate.maincode
                        axios.put(`/api/MainAccount/updateMainAccount`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if (response.data.responseCode == '200')
                            {
                                 this.submitorUpdate = 'Submit'
                                 this.postBody.maincode = '';this.postBody.description = '';
                                this.postBody.npf_balsheet_bl_code = ''; this.postBody.subtype = '';
                                this.postBody.subtype = '';
                                this.$store.state.objectToUpdate = "Update";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }    
            }
        }
    }
};
</script>