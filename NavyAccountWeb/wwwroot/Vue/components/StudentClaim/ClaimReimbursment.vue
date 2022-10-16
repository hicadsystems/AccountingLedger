<template>
    <div>
    <div class="card-body">
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
     <div class="row">
        <div class="col-12 col-xl-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-4">
                            <label class="form-label">School Name</label>
                                <select class="form-control" v-model="postBody.schoolname" name="schoolname" required>
                                    <option v-for="sch in schoolList" v-bind:value="sch.schoolname" v-bind:key="sch.schoolname"> {{ sch.schoolname }} </option>
                                </select>
                        </div>
                        <div class="col-md-4">
                                <label class="form-label">Voucher Number</label>
                                <input class="form-control" name="VoucherNumber" v-model="postBody.VoucherNumber" />
                        </div>
                        <!-- <div class="col-12 col-xl-2">
                            
                            <button class="btn btn-submit btn-primary" v-on:click="printProposal(postBody.schoolname)" type="submit">Search</button>
                        </div> -->

                        <div class="col-12 col-xl-2">
                            <button class="btn btn-submit btn-success" v-on:click="UpdateClaim(postBody.schoolname,postBody.VoucherNumber)" type="submit">Update</button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
       
        <table id="datatables-buttons" class="table table-striped" style="width:100%">
            <thead>
                <tr>
                    <th>Registration Number</th>
                    <th>Full Name</th>
                    <th>Parent Name</th>
                    <th>Sex</th>
                    <th>Age</th>
                    <th>School Name</th>
                    <th>School Type</th>
                    <th>Class</th>
                    <th>Amount</th>
                    <th>School Fee</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="student in studentList">
                    <td>{{ student.reg_Number }}</td>
                    <td>{{ student.surname}}</td>
                    <td>{{ student.parentName }}</td>
                    <td>{{ student.sex }}</td>
                    <td>{{ student.age }}</td>
                    <td>{{ student.schoolname }}</td>
                    <td>{{ student.schoolType }}</td>
                    <td>{{ student.className }}</td>
                    <td>{{ student.claimAmount }}</td>
                    <td>{{ student.feeAmount }}</td>
                    
                    <td><button type="button" class="btn btn-submit btn-primary" @click="processDelete(sch.id)">Delete</button></td>
                </tr>
            </tbody>

        </table>
        <!-- <paginate
           
            :page-count="getPageCount"
            :page-range="3"
            :margin-pages="2"
            :click-handler="clickCallback"
            :prev-text="'Prev'"
            :next-text="'Next'"
            class="pagination"
            :container-class="'pagination'"
            :page-class="'page-item'">
        </paginate>  -->
    </div>
    

</div>
</template>
<script>
    import Axios from 'axios';
    import Paginate from 'vuejs-paginate';
    export default {
        components:{
            Paginate,
            Axios
        },
    data() {
        return {
        responseMessage:'',
        paymentProposalList:null,
        schoolList: null,
        studentList:null,
        pageno:0,
        totalcount:0,
        ID:0,
        pp:'',
        postBody:{
            schoolname:'',
            VoucherNumber:'',
        }
        };

    },
    mounted(){
        Axios
        .get('/api/SchoolRecord/GetAll')
        .then(response => (this.schoolList = response.data));

        this.getAllClaim();
    },
    methods: {
        getAllClaim:function(){
        Axios
        .get('/api/StudentClaim/GetStudentsOnClaim')
        .then(response => {this.studentList = response.data
        console.log(this.studentList)
        });
        },
        printProposal:function(schoolname){
            Axios
            .get(`/api/StudentClaim/getCliamBySchool/${schoolname}`)
            .then(response => {this.studentList = response.data;
            })
        },
        UpdateClaim:function(schoolname,VoucherNumber){
            if(schoolname===''){
                schoolname='NULL'
            }
            Axios.get(`/api/StudentClaim/UpdateLedgerBySchool/${schoolname}/${VoucherNumber}`)
            .then(response=>{
                if(response.data.responseCode='200'){
                    this.responseMessage = response.data.responseDescription;
                    this.getAllClaim();
                }
               
            }).catch(e=>{
                this.error.push(e);
            });
            },
            processDelete:function(id){
            alert(id)
            Axios.get(`/api/StudentClaim/DeleteRecord/${id}`)
            .then(response=>{
                if(response.data.responseCode='200'){
                    alert("Successfully Deleted");
                    this.getAllClaim();
                }
            }).catch(e=>{
                this.error.push(e);
            });
      }
      }
    }
</script>
<style lang="css">

.pagination{display:inline-block;padding-left:0;margin:20px 0;border-radius:4px}.pagination>li{display:inline}.pagination>li>a,.pagination>li>span{position:relative;float:left;padding:6px 12px;margin-left:-1px;line-height:1.42857143;color:#337ab7;text-decoration:none;background-color:#fff;border:1px solid #ddd}.pagination>li>a:focus,.pagination>li>a:hover,.pagination>li>span:focus,.pagination>li>span:hover{z-index:2;color:#23527c;background-color:#eee;border-color:#ddd}.pagination>li:first-child>a,.pagination>li:first-child>span{margin-left:0;border-top-left-radius:4px;border-bottom-left-radius:4px}.pagination>li:last-child>a,.pagination>li:last-child>span{border-top-right-radius:4px;border-bottom-right-radius:4px}.pagination>.active>a,.pagination>.active>a:focus,.pagination>.active>a:hover,.pagination>.active>span,.pagination>.active>span:focus,.pagination>.active>span:hover{z-index:3;color:#fff;cursor:default;background-color:#337ab7;border-color:#337ab7}.pagination>.disabled>a,.pagination>.disabled>a:focus,.pagination>.disabled>a:hover,.pagination>.disabled>span,.pagination>.disabled>span:focus,.pagination>.disabled>span:hover{color:#777;cursor:not-allowed;background-color:#fff;border-color:#ddd}.pagination-lg>li>a,.pagination-lg>li>span{padding:10px 16px;font-size:18px;line-height:1.3333333}.pagination-lg>li:first-child>a,.pagination-lg>li:first-child>span{border-top-left-radius:6px;border-bottom-left-radius:6px}.pagination-lg>li:last-child>a,.pagination-lg>li:last-child>span{border-top-right-radius:6px;border-bottom-right-radius:6px}.pagination-sm>li>a,.pagination-sm>li>span{padding:5px 10px;font-size:12px;line-height:1.5}.pagination-sm>li:first-child>a,.pagination-sm>li:first-child>span{border-top-left-radius:3px;border-bottom-left-radius:3px}.pagination-sm>li:last-child>a,.pagination-sm>li:last-child>span{border-top-right-radius:3px;border-bottom-right-radius:3px}.pager{padding-left:0;margin:20px 0;text-align:center;list-style:none}.pager li{display:inline}.pager li>a,.pager li>span{display:inline-block;padding:5px 14px;background-color:#fff;border:1px solid #ddd;border-radius:15px}.pager li>a:focus,.pager li>a:hover{text-decoration:none;background-color:#eee}.pager .next>a,.pager .next>span{float:right}.pager .previous>a,.pager .previous>span{float:left}.pager .disabled>a,.pager .disabled>a:focus,.pager .disabled>a:hover,.pager .disabled>span{color:#777;cursor:not-allowed;background-color:#fff}

</style> 